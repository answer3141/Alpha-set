/// <summary>
/// 接続される対象になるエリアのインターフェース
/// </summary>
public interface IConnectionTargetArea
{
    void Connected(float currentPower);
}
