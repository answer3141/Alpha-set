using UnityEngine;
using System.Collections.Generic;

public class LightStatus : MonoBehaviour, ICableConnectable
{
    //状態参照する(隣の)ケーブルの子オブジェクトの通電状態を示す黄色いオブジェクトをアタッチ
    [SerializeField] private GameObject StatusObject;
    //電球の子オブジェクトをアタッチ
    [SerializeField] private GameObject OffLight;

    
    private void OnEnable() 
    {
        CableEventManager.OnInitializePowerStatus += InitializePower;
        CableEventManager.OnResetPowerStatus += ResetPowerStatus;
    }
    private void OnDisable() 
    {
        CableEventManager.OnInitializePowerStatus -= InitializePower;
        CableEventManager.OnResetPowerStatus -= ResetPowerStatus;
    }
    
    public void ConnectCable(List<IConnectionTriggerArea> targetCableList, float currentPower)
    {
        Debug.Log("called ConnectCable");

        if (currentPower <= 0) return;
        
        Debug.Log("LightStatus is powered on!");
        OffLight.SetActive(false);
    }

    public void InitializePower()
    {
        OffLight.SetActive(true);
    }

    public void ResetPowerStatus()
    {
        OffLight.SetActive(true);
    }

}


