using UnityEngine;
using System.Collections;

public class ClearCheck : MonoBehaviour
{
    // クリア用電球のリスト
    [SerializeField] private GameObject[] targets;//電球のプレハブの子オブジェクトのOff状態単体の電球をアタッチ
    [SerializeField] private GameObject clearPanel;
    [SerializeField] private Transform Canvas;

    private float delayTime = 1f; // クリアパネル表示後の遅延時間

    private bool isCleared = false;

    private void OnEnable()
    {
        CableEventManager.OnInitializePowerStatus += InitializeClearCondition;
        CableEventManager.OnCheckClearCondition += CheckClearCondition;
    }

    private void OnDisable()
    {
        CableEventManager.OnInitializePowerStatus -= InitializeClearCondition;
        CableEventManager.OnCheckClearCondition -= CheckClearCondition;
    }

    private void InitializeClearCondition()
    {
        isCleared = false;
    }

    private void CheckClearCondition()
    {
        if (isCleared) return; // すでにクリアしている場合はチェックをスキップ

        if (CheckAllActive()==true)
        {
            StartCoroutine(ShowClearPanelWithDelay());
        }
    }

    private IEnumerator ShowClearPanelWithDelay()
    {
        isCleared = true; // クリア状態を設定して重複表示を防止
        yield return new WaitForSeconds(delayTime);
        ShowClearPanel();
    }

    private bool CheckAllActive()
    {
        if (targets.Length == 0) return false;

        foreach (GameObject obj in targets)
        {
            if (obj == null) continue;

            if (obj.activeSelf)
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