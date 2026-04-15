using UnityEngine;
using System.Collections.Generic;

public class LightConnectionCheckArea : ConnectionCheckAreaBase, IConnectionTargetArea
{
    public void Connected(float currentPower)
    {
        base.parentConnectable.ConnectCable(new List<IConnectionTriggerArea>(), currentPower);
    }
}
