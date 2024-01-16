using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewData", menuName = "MyGame/Data", order = 1)]
public class scriptableObject : ScriptableObject
{
    public List<levelData> dataList = new List<levelData>();
}

[System.Serializable]
public class levelData
{
    public string taskName;
    public float taskTimer;
    public int wattage;
    public bool taskStats;
    public bool taskOpen;
    public bool taskWait;
    public bool taskActive;
}