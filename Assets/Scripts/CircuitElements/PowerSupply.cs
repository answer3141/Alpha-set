using UnityEngine;
using System.Collections.Generic;

public class PowerSupply : MonoBehaviour, ICableConnectable
{
    [SerializeField]
    private List<ConnectionCheckArea> connectedCableList = new List<ConnectionCheckArea>();
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
        if (otherObj.TryGetComponent(out ConnectionCheckArea connectionCheckArea))
        {
            connectedCableList.Add(connectionCheckArea);
        }
        
    }
    private void OnTriggerExit2D(Collider2D other) {
        GameObject otherObj = other.gameObject;
        if (otherObj.TryGetComponent(out ConnectionCheckArea connectionCheckArea))
        {
            connectedCableList.Remove(connectionCheckArea);
        }
    }
    public void ConnectCable(List<ConnectionCheckArea> targetCableList, float currentPower)
    {
        
        mySpriteRenderer.color = Color.yellow;
        foreach (ConnectionCheckArea targetCable in targetCableList)
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
            ConnectCable(connectedCableList, powerOutput);
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
