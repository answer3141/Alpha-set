using System;
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
    [SerializeField]
    private LayerMask alphabetCableLayer;
    private Dictionary<char, GameObject> alphabetCableDictionary;

    // 処理をキャンセルできるように、入力中に元のケーブルを保持するための変数
    private GameObject tempHiddenCable;
    // 入力中を示すための変数、入力が完了したらnullに戻す
    private GameObject typingCableInstance;
    // 現在入力対象のアルファベットケーブルの親オブジェクトを保持する変数、入力が完了したらnullに戻す
    private GameObject targetAlphabetParentObject;

    private void Awake()
    {
        // ScriptableObjectからデータを辞書型に登録
        alphabetCableDictionary = new Dictionary<char, GameObject>();
        foreach (var alphabetData in alphabetCableDataAsset.alphabetCableDataList)
        {
            alphabetCableDictionary.Add(alphabetData.character, alphabetData.prefab);
        }
        alphabetCableDictionary.Add('.', defaultPrefab);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!IsPointerOnAlphabetCable())
            {
                ResetCableTypingStatus();
            }
        }
    }

    private bool IsPointerOnAlphabetCable()
    {
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(worldMousePos.x, worldMousePos.y);
        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero, Mathf.Infinity, alphabetCableLayer);

        return hit.collider != null && hit.collider.gameObject.TryGetComponent(out AlphabetClickHandler _);
    }

    public void ChangeAlphabetCable(char targetChar)
    {
        if (!alphabetCableDictionary.ContainsKey(targetChar))
        {
            Debug.LogWarning($"{targetChar}に対応するアルファベットケーブルが見つかりませんでした。");
            targetChar = '.'; // デフォルトのプレハブを使用
        }
        foreach (Transform child in targetAlphabetParentObject.transform)
        {
            Destroy(child.gameObject);
        }
        GameObject alphabetCable = Instantiate(alphabetCableDictionary[targetChar], targetAlphabetParentObject.transform.position, Quaternion.identity, targetAlphabetParentObject.transform);
        alphabetCable.transform.SetParent(targetAlphabetParentObject.transform);
        ResetCableTypingStatus();

        CableEventManager.TriggerUpdatePowerStatus();
    }
    public void StartTypingReady(GameObject targetGameObject)
    {
        if (targetAlphabetParentObject == targetGameObject)
        {
            return;
        } else 
        {
            ResetCableTypingStatus();
            targetAlphabetParentObject = targetGameObject;
        }   

        // 元のケーブルを非表示にする
        tempHiddenCable = targetGameObject.transform.GetChild(0).gameObject;
        tempHiddenCable.SetActive(false);

        typingCableInstance = Instantiate(alphabetTypingPrefab, targetAlphabetParentObject.transform.position, Quaternion.identity, targetAlphabetParentObject.transform);
        typingCableInstance.transform.SetParent(targetAlphabetParentObject.transform);

        CableEventManager.TriggerUpdatePowerStatus();
    }

    private void ResetCableTypingStatus()
    {
        targetAlphabetParentObject = null;

        if (typingCableInstance != null)
        {
            Destroy(typingCableInstance);
            typingCableInstance = null;
        }
        if (tempHiddenCable != null)
        {
            tempHiddenCable.SetActive(true);
            tempHiddenCable = null;
        }

        CableEventManager.TriggerUpdatePowerStatus();

    }
}
