using UnityEngine;
using System.Collections.Generic;

public class ConnectionCheckArea : MonoBehaviour
{
    [SerializeField, Header("このエリアと繋がっているエリアを設定")]
    private List<ConnectionCheckArea> connectedCableList = new List<ConnectionCheckArea>();
    
    [SerializeField, Header("現在繋がっているエリア")]
    private List<ConnectionCheckArea> currentConnectedCableList = new List<ConnectionCheckArea>();
    private ICableConnectable parentConnectable;
    private Collider2D myCollider;
    private ContactFilter2D contactFilter;
    private void Awake()
    {
        myCollider = GetComponent<Collider2D>();
        contactFilter = new ContactFilter2D();
        contactFilter.useTriggers = true;

        if (!transform.parent.TryGetComponent(out parentConnectable ))
        {
            Debug.LogError($"{transform.parent.name}にICableConnectableを実装したコンポーネントが必要です。");
        }
    }

    private void OnEnable() 
    {
        CableEventManager.OnResetPowerStatus += UpdateColliderStatus;
    }
    private void OnDisable() {
        CableEventManager.OnResetPowerStatus -= UpdateColliderStatus;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.TryGetComponent(out ConnectionCheckArea connectionCheckArea))
        {
            currentConnectedCableList.Add(connectionCheckArea);
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.TryGetComponent(out ConnectionCheckArea connectionCheckArea))
        {
            currentConnectedCableList.Remove(connectionCheckArea);
        }
    }

    // 通電するときに外から呼び出される関数
    public void Connected(float currentPower)
    {
        if(connectedCableList.Count <= 0) return;
        parentConnectable.ConnectCable(connectedCableList, currentPower);
    }
    //　接続されているエリアにさらに繋がっているエリアがあれば、そこも通電させるための関数
    public void CheckConnections(float newPower)
    {
        
        if(currentConnectedCableList.Count <= 0) return;
        foreach (ConnectionCheckArea connectedCable in currentConnectedCableList)
        {
            connectedCable.Connected(newPower);
        }
    }

// ドラッグ後はOnTriggerEnter2DやOnTriggerExit2Dが呼び出される前に処理したいので、OverlapColliderで強制的に接触状態を更新
    public void UpdateColliderStatus()
    {
        currentConnectedCableList.Clear();

        Physics2D.SyncTransforms();
        List<Collider2D> results = new List<Collider2D>();
        int colliderCount = Physics2D.OverlapCollider(myCollider, contactFilter, results);

        if (colliderCount <= 0) return;

        foreach (var collider in results)
        {
            if (collider.gameObject.TryGetComponent(out ConnectionCheckArea connectionCheckArea))
            {
                currentConnectedCableList.Add(connectionCheckArea);
            }
        }
    }

}
