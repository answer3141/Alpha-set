using UnityEngine;

public class AlphabetArea : MonoBehaviour, IAlphabetArea
{
    public Vector3 GetAreaCenterPosition()
    {
        return transform.localPosition;
    }
}
