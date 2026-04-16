using UnityEngine;
using System.Collections.Generic;

public class PowerSupply : MonoBehaviour
{
    [SerializeField]
    private List<IConnectionTargetArea> connectedCableList = new List<IConnectionTargetArea>();
    // この電源から出る電力の量
    [SerializeField]
    private float powerOutput = 5f;
    private bool isPowerOn = false;
    private SpriteRenderer mySpriteRenderer;

    private void Start() {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable() 
    {
        CableEventManager.OnInitializePowerStatus += InitializePower;
        CableEventManager.OnResetPowerStatus += ResetPowerStatus;
        CableEventManager.OnUpdatePowerStatus += UpdatePowerStatus;
    }
    private void OnDisable() 
    {
        CableEventManager.OnInitializePowerStatus -= InitializePower;
        CableEventManager.OnResetPowerStatus -= ResetPowerStatus;
        CableEventManager.OnUpdatePowerStatus -= UpdatePowerStatus;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        GameObject otherObj = other.gameObject;
        if (otherObj.TryGetComponent(out IConnectionTargetArea connectionCheckArea))
        {
            connectedCableList.Add(connectionCheckArea);
        }
        
    }
    private void OnTriggerExit2D(Collider2D other) {
        GameObject otherObj = other.gameObject;
        if (otherObj.TryGetComponent(out IConnectionTargetArea connectionCheckArea))
        {
            connectedCableList.Remove(connectionCheckArea);
        }
    }
    public void PowerOn(List<IConnectionTargetArea> targetCableList, float currentPower)
    {
        
        mySpriteRenderer.color = Color.yellow;
        foreach (IConnectionTargetArea targetCable in targetCableList)
        {
            targetCable.Connected(currentPower);
        }
    }
    public void InitializePower()
    {
        isPowerOn = false;
        mySpriteRenderer.color = Color.white;
    }
    public void ResetPowerStatus()
    {
        mySpriteRenderer.color = Color.white;
    }
    public void UpdatePowerStatus()
    {
        if (isPowerOn)
        {
            PowerOn(connectedCableList, powerOutput);
        }
    }
    private void Update() {
        
        if(Input.GetKeyDown(KeyCode.Space))
        {
            isPowerOn = !isPowerOn;
            CableEventManager.TriggerUpdatePowerStatus();
        }
    }
}
