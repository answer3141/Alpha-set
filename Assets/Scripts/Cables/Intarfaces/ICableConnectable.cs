using UnityEngine;
using System.Collections.Generic;

// 通電処理を行うオブジェクトが実装するインターフェース
public interface ICableConnectable
{
    void ConnectCable(List<ConnectionCheckArea> targetCableList, float currentPower);
    void InitializePower();
    void ResetPowerStatus();
}
