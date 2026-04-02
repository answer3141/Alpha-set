using UnityEngine;

public class AlphabetArea : MonoBehaviour, IAlphabetArea
{
   public bool IsOccupied { get; set; } = false;
    public Vector3 GetAreaCenterPosition()
    {
        return transform.localPosition;
    }
}
