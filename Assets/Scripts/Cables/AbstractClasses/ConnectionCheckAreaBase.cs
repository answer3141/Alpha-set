using UnityEngine;
using System.Collections.Generic;

public abstract class ConnectionCheckAreaBase : MonoBehaviour
{
    // 接続処理をする親オブジェクトにあるインターフェース
    protected ICableConnectable parentConnectable;
    protected Collider2D myCollider;
    // コードからCollider2Dの接触状態を更新するためのContactFilter2D
    protected ContactFilter2D contactFilter;

    [SerializeField, Header("現在繋がっているエリア")]
    protected List<IConnectionTargetArea> currentConnectionTargetArea = new List<IConnectionTargetArea>();


    protected virtual void Awake()
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
        if (other.gameObject.TryGetComponent(out IConnectionTargetArea connectionCheckArea))
        {
            currentConnectionTargetArea.Add(connectionCheckArea);
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.TryGetComponent(out IConnectionTargetArea connectionCheckArea))
        {
            currentConnectionTargetArea.Remove(connectionCheckArea);
        }
    }
    
    // ドラッグ後はOnTriggerEnter2DやOnTriggerExit2Dが呼び出される前に処理したいので、OverlapColliderで強制的に接触状態を更新
    public void UpdateColliderStatus()
    {
        currentConnectionTargetArea.Clear();

        Physics2D.SyncTransforms();
        List<Collider2D> results = new List<Collider2D>();
        int colliderCount = Physics2D.OverlapCollider(myCollider, contactFilter, results);

        if (colliderCount <= 0) return;

        foreach (var collider in results)
        {
            if (collider.gameObject.TryGetComponent(out IConnectionTargetArea connectionCheckArea))
            {
                currentConnectionTargetArea.Add(connectionCheckArea);
            }
        }
    }

}
