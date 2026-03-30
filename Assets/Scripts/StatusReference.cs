using System.Runtime.Serialization;
using System.Transactions;
using UnityEngine;

public class StatusReference : MonoBehaviour
{
    //状態参照する(隣の)オブジェクトをアタッチ
    [SerializeField] private GameObject StatusObject;
    //コード自身の通電状態の表示に使う黄色い子オブジェクトをアタッチ
    [SerializeField] private GameObject PowerStatus;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(PowerStatus != null)
        {
            PowerStatus.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            StatusControl();
        }
    }

    public void StatusControl()
    {
        if (StatusObject != null)
        {
            if (StatusObject.CompareTag("Supply"))
            {
                PowerStatus.SetActive(true);
            }
            else if (StatusObject.activeSelf == false)
            {
                PowerStatus.SetActive(false);
            }
            else if (StatusObject.activeSelf == true)
            {
                PowerStatus.SetActive(true);
            }
        }
    }
}
