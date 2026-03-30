using UnityEngine;
using System.Collections.Generic;

public class NormalCable : MonoBehaviour, ICableConnectable
{
    // このケーブルを通るときの電力損失量
    [SerializeField]
    private int powerLossAmount = 1;
    
    //コード自身の通電状態の表示に使う黄色い子オブジェクトをアタッチ
    [SerializeField] 
    private GameObject powerStatus;

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
    void Start()
    {
        powerStatus.SetActive(false);
    }

    public void ConnectCable(List<ConnectionCheckArea> targetCableList, float currentPower)
    {
        if (currentPower <= 0) return;
        powerStatus.SetActive(true);
        foreach (ConnectionCheckArea targetCable in targetCableList)
        {
            targetCable.CheckConnections(currentPower - powerLossAmount);
        }
    }
    public void InitializePower()
    {
        powerStatus.SetActive(false);
    }
    public void ResetPowerStatus()
    {
        powerStatus.SetActive(false);
    }
}
