using System.Collections.Generic;
using UnityEngine;

public class AlphabetChanger : MonoBehaviour
{
    [SerializeField]
    private AlphabetCableDataAsset alphabetCableDataAsset;
    [SerializeField]
    private GameObject alphabetTypingPrefab;
    [SerializeField]
    private GameObject defaultPrefab;
    private Dictionary<char, GameObject> alphabetCableDictionary;

    private void Awake()
    {
        alphabetCableDictionary = new Dictionary<char, GameObject>();
        foreach (var alphabetData in alphabetCableDataAsset.alphabetCableDataList)
        {
            alphabetCableDictionary.Add(alphabetData.character, alphabetData.prefab);
        }
        alphabetCableDictionary.Add('_', alphabetTypingPrefab);
        alphabetCableDictionary.Add('.', defaultPrefab);
    }

    /// <summary>
    /// '_': 入力中用のプレハブ
    /// '.': デフォルトのプレハブ
    /// </summary>
    public void ChangeAlphabetCable(char targetChar, GameObject targetGameObject)
    {
        foreach (Transform child in targetGameObject.transform)
        {
            Destroy(child.gameObject);
        }
        GameObject alphabetCable = Instantiate(alphabetCableDictionary[targetChar], targetGameObject.transform.position, Quaternion.identity, targetGameObject.transform);
        alphabetCable.transform.SetParent(targetGameObject.transform);

        CableEventManager.TriggerUpdatePowerStatus();
        Debug.Log("Alphabet changed to: " + targetChar);
    }
}
