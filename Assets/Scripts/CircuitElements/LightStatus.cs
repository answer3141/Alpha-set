using UnityEngine;

public class LightStatus : MonoBehaviour
{
    //状態参照する(隣の)ケーブルの子オブジェクトの通電状態を示す黄色いオブジェクトをアタッチ
    [SerializeField] private GameObject StatusObject;
    //電球の子オブジェクトをアタッチ
    [SerializeField] private GameObject OffLight;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StatusControl();
        }
    }


    public void StatusControl()
    {
        if (StatusObject != null && OffLight != null)
        {
        
            if (StatusObject.activeSelf == false)
            {
                OffLight.SetActive(true);
            }
            else
            {
                OffLight.SetActive(false);
            }
        }
    }
}


