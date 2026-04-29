using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PointerDebugger : MonoBehaviour
{
    void Update()
    {
        // マウスの左クリックが押された瞬間に調査を実行
        if (Input.GetMouseButtonDown(0))
        {
            // EventSystemが存在しない場合はエラーを防ぐ
            if (EventSystem.current == null)
            {
                Debug.LogError("シーンにEventSystemがありません！Hierarchyから追加してください。");
                return;
            }

            // 現在のマウス位置をセットしたイベントデータを作成
            PointerEventData pointerData = new PointerEventData(EventSystem.current)
            {
                position = Input.mousePosition
            };

            // Raycastの結果を格納するリスト
            List<RaycastResult> results = new List<RaycastResult>();
            
            // マウス位置から画面奥に向かってRaycastを飛ばし、当たったものを全て取得
            EventSystem.current.RaycastAll(pointerData, results);

            if (results.Count > 0)
            {
                // results[0] が、一番手前で判定を吸い取っているオブジェクト
                Debug.Log($"【一番手前にあるオブジェクト】: {results[0].gameObject.name}");
                Debug.Log($"【判定したモジュール】: {results[0].module.GetType().Name}");
            }
            else
            {
                // 何もヒットしなかった場合
                Debug.LogWarning("ポインターの下には、クリック判定を受け取れるオブジェクトが何もありません！");
            }
        }
    }
}