using UnityEngine;

public class ClearCheck : MonoBehaviour
{
    // クリア用電球のリスト
    [SerializeField] private GameObject[] targets;
    [SerializeField] private GameObject clearPanel;
    [SerializeField] private Transform Canvas;

    private bool isCleared = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && !isCleared)
        {
           if (CheckAllActive()==true)
           {
               // クリア処理
               ShowClearPanel();
           }
        }
    }

    private bool CheckAllActive()
    {
        if (targets.Length == 0) return false;

        foreach (GameObject obj in targets)
        {
            if (obj == null) continue;

            if (!obj.activeSelf)
            {
                return false;
            }
        }

        return true;
    }

    private void ShowClearPanel()
    {
        if (clearPanel != null && Canvas != null)
        {
            isCleared = true;

            // 【修正ポイント】Instantiate を使って「実体」を作り、同時に親を設定します
            GameObject instantiatedPanel = Instantiate(clearPanel, Canvas);

            // UIの座標を中央にリセット（念のため）
            RectTransform rect = instantiatedPanel.GetComponent<RectTransform>();
            if (rect != null)
            {
                rect.anchoredPosition = Vector2.zero; // 真ん中に配置
                rect.localScale = Vector3.one;       // サイズを1倍に
            }
        }
    }
}


   
