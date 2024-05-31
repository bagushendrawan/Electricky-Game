using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SingletonDataScript : MonoBehaviour
{
    [HideInInspector] public bool global_win_Q = false;
    [HideInInspector] public bool global_lose_Q = false;

    private float timer;
    public static int eleCapacity;
    private float eleQuota;
    private bool eleGlobal;
    public static SingletonDataScript singletonInstance;
    [SerializeField] private ScriptableObjectScript script_scriptable;
    [HideInInspector] public List<LevelDataClass> level_listDataInstanceBackup;

    private void Awake()
    {
        if (singletonInstance == null)
        {
            singletonInstance = this;
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
        level_listDataInstanceBackup = new List<LevelDataClass>();
        level_listDataInstanceBackup = CloneList(script_scriptable.global_tronicDataList);
        timer = script_scriptable.global_timer;
        eleCapacity = script_scriptable.global_eleCapacity;
        eleQuota = script_scriptable.global_eleQuota;
        eleGlobal = script_scriptable.global_eleOn_Q;
    }

    
    private void OnDisable()
    {
        script_scriptable.global_tronicDataList = CloneList(level_listDataInstanceBackup);
        script_scriptable.global_timer = timer;
        script_scriptable.global_eleCapacity = eleCapacity;
        script_scriptable.global_eleQuota = eleQuota;
        script_scriptable.global_eleOn_Q = eleGlobal;
    }

    public List<LevelDataClass> CloneList(List<LevelDataClass> originalList)
    {
        List<LevelDataClass> clonedList = new List<LevelDataClass>();

        foreach (LevelDataClass originalObject in originalList)
        {
            // Create a deep copy of each object and add it to the cloned list
            clonedList.Add(originalObject.CopyFrom(originalObject));
        }

        return clonedList;
    }
}
