using UnityEngine;
using System.Collections.Generic;

public class ResetPosition : MonoBehaviour
{
    [SerializeField]
    private GameObject targetObjectParent;
    private List<GameObject> targetObjects = new List<GameObject>();

    private struct ObjectData
    {
        public Vector2 position;
        public float rotation;
    }

    private List<ObjectData> originalData = new List<ObjectData>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        targetObjects.Clear();
        originalData.Clear();
        foreach (Transform child in targetObjectParent.transform)
        {
            GameObject obj = child.gameObject;
            targetObjects.Add(obj);
            if (obj != null)
            {
                originalData.Add(new ObjectData
                {
                position = obj.transform.position,
                rotation = obj.transform.rotation.eulerAngles.z
                }); 
            }
        
        
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            for (int i = 0; i < targetObjects.Count; i++)
            {
                if (targetObjects[i] != null)
                {
                    targetObjects[i].transform.position = originalData[i].position;
                    targetObjects[i].transform.rotation = Quaternion.Euler(0, 0, originalData[i].rotation);
                }
            }
            CableEventManager.TriggerUpdatePowerStatus();
        }
        
    }
}
