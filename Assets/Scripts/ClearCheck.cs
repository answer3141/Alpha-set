using UnityEngine;

public class ClearCheck : MonoBehaviour
{
    // クリア用電球のリスト
    [SerializeField] private GameObject[] targets;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
           if (CheckAllActive()==true)
           {
               // クリア処理
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
}