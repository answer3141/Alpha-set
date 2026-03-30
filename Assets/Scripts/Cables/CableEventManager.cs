using System;

public static class CableEventManager
{
    // 電力の初期化
    public static event Action OnInitializePowerStatus;
    // 電力をいったんOFFにし、再度接続判定する
    public static event Action OnResetPowerStatus;
    public static event Action OnUpdatePowerStatus;
    public static void TriggerInitializePowerStatus()
    {
        OnInitializePowerStatus?.Invoke();
    }
    public static void TriggerUpdatePowerStatus()
    {
        OnResetPowerStatus?.Invoke();
        OnUpdatePowerStatus?.Invoke();
    }
}
