using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptableReference : MonoBehaviour
{
    public scriptableObject dataListScriptableObject;

    void Start()
    {
        // Accessing the list
        List<levelData> dataList = dataListScriptableObject.dataList;

        // Accessing individual elements
        foreach (levelData data in dataList)
        {
            Debug.Log("Item: " + data.taskName + ", Value: " + data.taskTimer + ", Stats: " + data.taskStats);
        }
    }
}
