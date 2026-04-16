using UnityEngine;
using System.Collections.Generic;

public class NormalCable : MonoBehaviour, ICableConnectable
{
    // このケーブルを通るときの電力損失量。ただし、増幅装置の場合はマイナスで対応。
    [SerializeField]
    private float powerLossAmount = 1f;
    
    //コード自身の通電状態の表示に使う黄色い子オブジェクトをアタッチ
    [SerializeField] 
    private GameObject powerStatus;
    [SerializeField]
    private float currentPower = 0f;

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

    public void ConnectCable(List<IConnectionTriggerArea> targetCableList, float previousPower)
    {
        if (previousPower <= 0 || previousPower < powerLossAmount) return;
        powerStatus.SetActive(true);

        currentPower = previousPower;
        foreach (IConnectionTriggerArea targetCable in targetCableList)
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
