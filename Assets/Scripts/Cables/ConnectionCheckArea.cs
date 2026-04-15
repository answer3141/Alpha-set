using UnityEngine;
using System.Collections.Generic;
using System.Linq;

// IConnectionTargetArea と IConnectionTriggerArea
public class ConnectionCheckArea : ConnectionCheckAreaBase, IConnectionTargetArea, IConnectionTriggerArea
{
    [SerializeField, Header("このエリアと繋がっているエリアを設定")]
    private List<MonoBehaviour> connectedCableObjects = new List<MonoBehaviour>();
    private List<IConnectionTriggerArea> connectedCableList = new List<IConnectionTriggerArea>();

    protected override void Awake()
    {
        base.Awake();
        connectedCableList = connectedCableObjects
            .Where(obj => obj != null)
            .OfType<IConnectionTriggerArea>()
            .ToList();
    }

    // 通電するときに外から呼び出される関数
    public void Connected(float currentPower)
    {
        Debug.Log("called Connected in ConnectionCheckArea");
        if(connectedCableList.Count <= 0) return;
        base.parentConnectable.ConnectCable(connectedCableList, currentPower);
    }
    //　接続されているエリアにさらに繋がっているエリアがあれば、そこも通電させるための関数
    public void CheckConnections(float newPower)
    {
        
        if(base.currentConnectionTargetArea.Count <= 0) return;
        foreach (IConnectionTargetArea connectedCable in base.currentConnectionTargetArea)
        {
            connectedCable.Connected(newPower);
        }
    }

#if UNITY_EDITOR
    private void OnValidate() 
    {
        for (int i = 0; i < connectedCableObjects.Count; i++)
        {
            MonoBehaviour obj = connectedCableObjects[i];
            if (obj != null && !(obj is IConnectionTargetArea))
            {
                Debug.LogWarning($"connectedCableObjectsの要素にはIConnectionTargetAreaを実装したコンポーネントをアタッチしてください。{obj.name}は無効化されます。");
                connectedCableObjects[i] = null;
            }
        }
    }
#endif

}
