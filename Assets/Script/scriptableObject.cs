using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewData", menuName = "MyGame/Data", order = 1)]
public class scriptableObject : ScriptableObject
{
    public List<levelData> dataList = new List<levelData>();
}
