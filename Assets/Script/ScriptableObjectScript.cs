using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLevel", menuName = "MyLevel/Data", order = 1)]
public class ScriptableObjectScript : ScriptableObject
{
    public int global_eleCapacity;
    public float global_eleQuota;
    public float global_timer;
    public bool global_eleOn_Q;
    public List<LevelDataClass> global_tronicDataList = new List<LevelDataClass>();
}

[System.Serializable]
public class LevelDataClass
{
    public string tronic_name;
    public float tronic_timer;
    public int tronic_wattage;
    public int tronic_wattPerSec;
    public bool tronic_statsDone_Q;
    public bool tronic_couldRestored_Q;
    public bool tronic_active_Q;
    public bool tronic_eleSupplied_Q;
    public bool tronic_correct_Q;
    public Coroutine tronic_timerCoroutine;

    public LevelDataClass CopyFrom(LevelDataClass level_originalData)
    {
        // Create a new instance of levelData
        LevelDataClass level_copiedData = new LevelDataClass();

        // Copy properties from the original object to the new object
        level_copiedData.tronic_name = level_originalData.tronic_name;
        level_copiedData.tronic_timer = level_originalData.tronic_timer;
        level_copiedData.tronic_wattage = level_originalData.tronic_wattage;
        level_copiedData.tronic_wattPerSec = level_originalData.tronic_wattPerSec;
        level_copiedData.tronic_statsDone_Q = level_originalData.tronic_statsDone_Q;
        level_copiedData.tronic_couldRestored_Q = level_originalData.tronic_couldRestored_Q;
        level_copiedData.tronic_active_Q = level_originalData.tronic_active_Q;
        level_copiedData.tronic_eleSupplied_Q = level_originalData.tronic_eleSupplied_Q;
        level_copiedData.tronic_correct_Q = level_originalData.tronic_correct_Q;
        level_copiedData.tronic_timerCoroutine = level_originalData.tronic_timerCoroutine;
        // Copy other properties as needed

        // Return the copied object
        return level_copiedData;
    }
}

