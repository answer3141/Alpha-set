/// <summary>
/// 他のケーブルへ接続できるエリアのインターフェース
/// </summary>
public interface IConnectionTriggerArea
{
    void CheckConnections(float newPower);
}
