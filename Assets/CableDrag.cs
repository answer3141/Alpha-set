using UnityEngine;

public class CableDrag : MonoBehaviour
{
    private Vector3 offset;
    private Camera mainCam;
    private bool isDragging = false;

    // スナップ設定 — Inspectorから変更可能
    [Header("Snap Settings")]
    public Vector3 snapPosition = new Vector3(0, 0, -1); // はまる座標
    public float snapRangeX = 60f;  // X方向の許容範囲（±）
    public float snapRangeY = 60f;  // Y方向の許容範囲（±）

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

    private void TrySnap()
    {
        float objx = transform.localPosition.x;
        float objy = transform.localPosition.y;

        float dx = Mathf.Abs(objx - snapPosition.x);
        float dy = Mathf.Abs(objy - snapPosition.y);

        if (dx <= snapRangeX && dy <= snapRangeY)
        {
            transform.localPosition = snapPosition;
            Debug.Log("Snapped: " + gameObject.name);
        }
    }
}