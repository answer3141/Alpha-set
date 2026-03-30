using UnityEngine;
using System.Collections.Generic;
public class CableDrag : MonoBehaviour
{
    private Vector3 offset;
    private Camera mainCam;
    private bool isDragging = false;

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

            RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                isDragging = true;
                offset = transform.position - worldPos;
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
        if (nearbyAreas.Count == 0) return;

        Vector3 currentPos = transform.localPosition;
        Vector3 snapPosition = nearbyAreas[0].GetAreaCenterPosition();
        // 距離の大小を比べたいだけなので、Distanceではなくルート処理がないSqrMagnitude(距離の二乗)で比較
        float currentDiff = Vector3.SqrMagnitude(currentPos - snapPosition);

        // 複数のエリアがある場合は、最も近いエリアにスナップさせる
        foreach (var area in nearbyAreas)
        {
            float newDiff = Vector3.SqrMagnitude(currentPos - area.GetAreaCenterPosition());
            if (newDiff < currentDiff)
            {
                snapPosition = area.GetAreaCenterPosition();
                currentDiff = newDiff;
            }
        }
        transform.localPosition = snapPosition;

    }
}