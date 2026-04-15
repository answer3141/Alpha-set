using UnityEngine;
/// <summary>
/// アルファベットを置けるエリアのインターフェース
/// 主にスナップさせる位置を取得するために用意
/// </summary>
public interface IAlphabetArea
{
    Vector3 GetAreaCenterPosition();
    
    bool IsOccupied { get; set; }
}