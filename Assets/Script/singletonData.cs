using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class singletonData : MonoBehaviour
{
    public float timer { get; set; }
    public int eleCapacity { get; set; }
    public float eleQuota { get; set; }

   [HideInInspector] public bool win = false;
   [HideInInspector] public bool lose = false;
    

    public static singletonData instance;
    public scriptableObject script_scriptable;
    public List<levelData> listDataInstance { get; set; }
    

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        Application.targetFrameRate = 60;
    }

    private void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        listDataInstance = new List<levelData>();
        listDataInstance = CloneList(script_scriptable.dataList);
        timer = script_scriptable.timer;
        eleCapacity = script_scriptable.eleCapacity;
        eleQuota = script_scriptable.eleQuota;
    }

    
    private void OnApplicationQuit()
    {
        script_scriptable.dataList = CloneList(listDataInstance);
        script_scriptable.timer = timer;
        script_scriptable.eleCapacity = eleCapacity;
        script_scriptable.eleQuota = eleQuota;
    }

    public List<levelData> CloneList(List<levelData> originalList)
    {
        List<levelData> clonedList = new List<levelData>();

        foreach (levelData originalObject in originalList)
        {
            // Create a deep copy of each object and add it to the cloned list
            clonedList.Add(originalObject.CopyFrom(originalObject));
        }

        return clonedList;
    }
}
