using UnityEngine;
using System.Collections.Generic;

public class AlphabetCable : MonoBehaviour, ICableConnectable
{
    // このケーブルを通るときの電力損失量
    [SerializeField]
    private int powerLossAmount = 1;
    
    //コード自身の通電状態の表示に使う黄色い子オブジェクトをアタッチ
    [SerializeField] 
    private GameObject powerStatus;
    private CableDrag cableDrag;

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
    void Awake()
    {
        powerStatus.SetActive(false);
        if (transform.parent.TryGetComponent(out CableDrag drag))
        {
            Debug.Log($"CableDrag found in parent: {transform.parent.name}");
            cableDrag = drag;
        }
        else
        {
            Debug.LogError($"CableDrag が {transform.parent.name} に見つかりません");
        }
    }

    public void ConnectCable(List<IConnectionTriggerArea> targetCableList, float currentPower)
    {
        Debug.Log($"Connecting cable with current power: {currentPower}");
        if (currentPower <= 0) return;
        // 未設置の際には通電させない
        if (cableDrag.CurrentArea == null) return;
        powerStatus.SetActive(true);
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
