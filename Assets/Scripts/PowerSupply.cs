using UnityEngine;
using System.Collections.Generic;

public class PowerSupply : MonoBehaviour, ICableConnectable
{
    [SerializeField]
    private List<ConnectionCheckArea> connectedCableList = new List<ConnectionCheckArea>();
    // この電源から出る電力の量
    [SerializeField]
    private float powerOutput = 5f;
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
        foreach (ConnectionCheckArea targetCable in targetCableList)
        {
            targetCable.Connected(currentPower);
        }
    }
    private void Update() {
        
        if(Input.GetKeyDown(KeyCode.Space))
        {
            ConnectCable(connectedCableList, powerOutput);
        }
    }
}
