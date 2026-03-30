using System.Runtime.Serialization;
using System.Transactions;
using UnityEngine;

public class LightStatus : MonoBehaviour
{
    //状態参照する(隣の)オブジェクトをアタッチ
    [SerializeField] private GameObject StatusObject;
    //電球の子オブジェクトをアタッチ
    [SerializeField] private GameObject OffLight;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
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
            if (StatusObject.activeSelf == false)
            {
                OffLight.SetActive(true);
            }
            else if (StatusObject.activeSelf == true)
            {
                OffLight.SetActive(false);
            }
        }
    }
}
