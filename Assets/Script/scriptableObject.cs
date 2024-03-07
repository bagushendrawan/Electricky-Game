using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewData", menuName = "MyGame/Data", order = 1)]
public class scriptableObject : ScriptableObject
{
    public int eleCapacity;
    public float eleQuota;
    public float timer;
    
    public bool ele { get; set; }
    public List<levelData> dataList = new List<levelData>();
    
}

[System.Serializable]
public class levelData
{
    public string taskName;

    public float taskTimer;
    public int wattage;
    public int wattPerSec;
    public bool taskStats;
    public bool taskRestore;
    public bool taskActive;
    public bool eleOn;

    public levelData CopyFrom(levelData original)
    {
        // Create a new instance of levelData
        levelData copiedData = new levelData();

        // Copy properties from the original object to the new object
        copiedData.taskName = original.taskName;
        copiedData.taskTimer = original.taskTimer;
        copiedData.wattage = original.wattage;
        copiedData.wattPerSec = original.wattPerSec;
        copiedData.taskStats = original.taskStats;
        copiedData.taskRestore = original.taskRestore;
        copiedData.taskActive = original.taskActive;
        copiedData.eleOn = original.eleOn;
        // Copy other properties as needed

        // Return the copied object
        return copiedData;
    }
}

