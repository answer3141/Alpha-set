using UnityEngine;
using System.Collections.Generic;
public class CableDrag : MonoBehaviour
{
    private Vector3 offset;
    private Camera mainCam;
    [SerializeField]
    private bool isDragging = false;

    [SerializeField]    
    private IAlphabetArea currentArea = null;
    [SerializeField]
    private LayerMask AlphabetLayerMask;

    // 一度に複数のエリアに入る可能性があるため、リストで管理
    private List<IAlphabetArea> nearbyAreas = new List<IAlphabetArea>();

    void Start()
    {
        mainCam = Camera.main;

        if (GetComponent<Collider2D>() == null)
        {
            gameObject.AddComponent<BoxCollider2D>();
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 screenPos = new Vector3(
                Input.mousePosition.x,
                Input.mousePosition.y,
                Mathf.Abs(mainCam.transform.position.z)
            );
            Vector3 worldPos = mainCam.ScreenToWorldPoint(screenPos);

            RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero, Mathf.Infinity, AlphabetLayerMask);

            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                isDragging = true;
                offset = transform.position - worldPos;

                    if (currentArea != null)
                    {
                        currentArea.IsOccupied = false;
                        currentArea = null;
                    }
            }
        }

        if (isDragging && Input.GetMouseButton(0))
        {
            Vector3 screenPos = new Vector3(
                Input.mousePosition.x,
                Input.mousePosition.y,
                Mathf.Abs(mainCam.transform.position.z)
            );
            transform.position = mainCam.ScreenToWorldPoint(screenPos) + offset;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            TrySnap();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        GameObject otherObj = other.gameObject;
        if (otherObj.GetComponent<IAlphabetArea>() == null) return;
        nearbyAreas.Add(otherObj.GetComponent<IAlphabetArea>());
    }

    private void OnTriggerExit2D(Collider2D other) {
        GameObject otherObj = other.gameObject;
        if (otherObj.GetComponent<IAlphabetArea>() == null) return;
        nearbyAreas.Remove(otherObj.GetComponent<IAlphabetArea>());
    }

    private void TrySnap()
    {
        IAlphabetArea bestArea = null;
        float minDiff = float.MaxValue;

        foreach (var area in nearbyAreas)
        {
            if (area.IsOccupied) continue;

            float diff = Vector3.SqrMagnitude(transform.localPosition - area.GetAreaCenterPosition());
            if (diff < minDiff)
            {
                minDiff = diff;
                bestArea = area;
            }
        }

        if (bestArea != null)
        {
            transform.localPosition = bestArea.GetAreaCenterPosition();
            bestArea.IsOccupied = true; 
            currentArea = bestArea;
        }
    
        CableEventManager.TriggerUpdatePowerStatus();
    }

}