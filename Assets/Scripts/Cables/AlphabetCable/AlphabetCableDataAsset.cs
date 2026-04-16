using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class AlphabetCableData
{
    public char character;
    public GameObject prefab;
}

[CreateAssetMenu(fileName = "AlphabetCableDataAsset", menuName = "ScriptableObjects/AlphabetCableDataAsset", order = 1)]
public class AlphabetCableDataAsset : ScriptableObject
{
    public List<AlphabetCableData> alphabetCableDataList = new List<AlphabetCableData>();
}
