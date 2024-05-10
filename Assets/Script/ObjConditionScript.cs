using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjConditionScript : MonoBehaviour
{
   [SerializeField] private ScriptableObjectScript script_scriptable;
    private SingletonDataScript script_singletonData;
    private TaskWaitTimerUIScript script_waitTimer;
    private TaskStatsUIScript script_taskUi;

    //Get global tronic data list and store it here
    [HideInInspector] public List<LevelDataClass> obj_dataList;

    //Show if errors
    private GameObject global_electricitySwitch;
    [HideInInspector] public List<GameObject> obj_assetSwitchList;
    [HideInInspector] public bool isElectricityAssetFound;

    /// <summary>
    /// Onscene Loaded 
    /// </summary>
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene loaded: " + scene.name);
        // THIS ONE IS A PROBLEM BECAUSE ON ENABLE IS CALLED FIRST THAN START
        assignObj();
        objColor();
    }

    private void Start()
    {
        script_waitTimer = GetComponent<TaskWaitTimerUIScript>();
        script_singletonData = GetComponent<SingletonDataScript>();
        script_taskUi = GetComponent<TaskStatsUIScript>();
        obj_dataList = script_scriptable.global_tronicDataList;
        global_electricitySwitch = GameObject.FindWithTag("Electricity");
        
    }

    private void Update()
    {
        loseCheck();
        eleSwitch();

        if (script_scriptable.global_eleCapacity <= 0)
        {
            //Debug.Log("Elec Out");
        }
        else
        {
            script_scriptable.global_eleOn_Q = true;
        }

        if (GameObject.FindWithTag("Electricity") != null  && !isElectricityAssetFound)
        {
            //Debug.Log("Find Electricity & Obj");
            global_electricitySwitch = GameObject.FindWithTag("Electricity");

            isElectricityAssetFound = true;
        }

    }
   
    //update all asset color inside obj_assetList
    public void objColor()
    {
        Debug.Log("Asset Switch " + obj_assetSwitchList.Count);
        Debug.Log("Asset Data " + obj_dataList.Count);
        for (int i = 0; i < obj_assetSwitchList.Count; i++)
        {
            if(obj_assetSwitchList != null && obj_dataList != null)
            {
                GameObject obj = obj_assetSwitchList[i];
                if (obj_dataList[i].tronic_eleSupplied_Q && !obj_dataList[i].tronic_active_Q)
                {
                    ChangeObjectMaterial(obj, Color.yellow);
                }

                if (obj_dataList[i].tronic_active_Q)
                {
                    ChangeObjectMaterial(obj, Color.green);

                    if (obj_dataList[i].tronic_active_Q && obj_dataList[i].tronic_timer <= 0)
                    {
                        ChangeObjectMaterial(obj, Color.blue);
                    }
                }

                if (!obj_dataList[i].tronic_eleSupplied_Q)
                {
                    ChangeObjectMaterial(obj, Color.white);
                }
            }
        }
    }

    //activate obj
    public void objSwitch(GameObject target)
    {
        //Debug.Log("Here");
        for (int i = 0; i < obj_dataList.Count; i++)
        {
            if(target.name == obj_assetSwitchList[i].name)
            {
                //Debug.Log($"Activated Found of index {i}");
                //Debug.Log("Object Activated");
                
                if (!obj_dataList[i].tronic_active_Q)
                {
                    obj_dataList[i].tronic_active_Q = !obj_dataList[i].tronic_active_Q;
                    script_scriptable.global_eleCapacity -= obj_dataList[i].tronic_wattage;
                    if (script_scriptable.global_eleCapacity < 0)
                    {
                        Debug.Log("Electricity Out");
                        elecOut();
                        objColor();
                        break;
                    }
                    
                    else
                    {
                        StartCoroutine(timerDecreasePerSec(i));
                        script_waitTimer.StartTimer(obj_dataList[i].tronic_timer, obj_dataList[i].tronic_name, i);
                        objColor();
                        break;
                    }
                }
                else
                {
                    
                    obj_dataList[i].tronic_active_Q = !obj_dataList[i].tronic_active_Q;
                    script_scriptable.global_eleCapacity += obj_dataList[i].tronic_wattage;
                    //Debug.Log("Object Deactivated");
                }
            }
        }
    }

    //turn off electricity
    public void elecOut()
    {
        script_scriptable.global_eleOn_Q = false;
        for (int i = 0; i < obj_dataList.Count; i++)
        {
            obj_dataList[i].tronic_eleSupplied_Q = false;
            obj_dataList[i].tronic_active_Q = false;
        }
    }

    //Check global ele then change its color
    public void eleSwitch()
    {
        if (global_electricitySwitch != null)
        {
            GameObject obj = global_electricitySwitch;
            Collider collider = obj.GetComponent<Collider>();
            if (script_scriptable.global_eleOn_Q)
            {
                ChangeObjectMaterial(obj, Color.yellow);
                collider.enabled = false;
            }
            else
            {
                ChangeObjectMaterial(obj, Color.cyan);
                collider.enabled = true;
            }
        }
    }

    //bring back electricity
    public void eleButton()
    {
        
        if(!script_scriptable.global_eleOn_Q)
        {
            Debug.Log("Elec On");
            for (int i = 0; i < obj_dataList.Count; i++)
            {
                obj_dataList[i].tronic_eleSupplied_Q = true;
                script_scriptable.global_eleCapacity += obj_dataList[i].tronic_wattage;
                objColor();
                if (obj_dataList[i].tronic_couldRestored_Q)
                {
                    obj_dataList[i].tronic_active_Q = true;
                    script_scriptable.global_eleCapacity -= obj_dataList[i].tronic_wattage;
                    objColor();
                }
            }
        }
    }

    public void winCheck()
    {
        List<bool> check = new List<bool>();
        for (int i = 0; i < obj_dataList.Count; i++)
        {
            check.Add(obj_dataList[i].tronic_statsDone_Q);
        }

        if (!check.Contains(false))
        {
            Debug.Log("You Win!");
            script_singletonData.global_win_Q = true;
        }
    }

    public void loseCheck()
    {
        if(script_scriptable.global_timer <= 0 || script_scriptable.global_eleQuota <= 0)
        {
            script_singletonData.global_lose_Q = true;
        }
    }

    IEnumerator eleDecreasePerSec(int index,float amount)
    {
        while (true && obj_dataList[index].tronic_active_Q)
        {
            script_scriptable.global_eleQuota -= amount * Time.deltaTime;
            if(obj_dataList[index].tronic_timer < 0)
            {
                yield break;
            }
            yield return null; // Wait for the next frame
        }
    }

    //Decrease the timer and update the task
    IEnumerator timerDecreasePerSec(int index)
    {
        if (index < 0 || index >= obj_dataList.Count)
        {
            Debug.LogError("Invalid index provided for timerDecreasePerSec coroutine.");
            yield break; // Exit the coroutine if the index is out of range
        }

        StartCoroutine(eleDecreasePerSec(index,obj_dataList[index].tronic_wattPerSec));

        while (obj_dataList[index].tronic_timer >= 0 && obj_dataList[index].tronic_active_Q)
        {
            obj_dataList[index].tronic_timer -= Time.deltaTime;
            if (obj_dataList[index].tronic_timer <= 0)
            {
                obj_dataList[index].tronic_statsDone_Q = !obj_dataList[index].tronic_statsDone_Q;
                script_taskUi.updateTask();
                objColor();
                //Debug.Log("Object Deactivated-Done Timer");
                winCheck();
                yield break; // Exit the coroutine if the taskTimer has reached zero
            }
            yield return null; // Wait for the next frame
        }
    }

    //Assign obj to objList
    public void assignObj()
    {
        if(obj_assetSwitchList != null)
        {
            obj_assetSwitchList.Clear();
        }
        
        foreach (GameObject x in GameObject.FindGameObjectsWithTag("Switch"))
        {
            obj_assetSwitchList.Add(x);
        }

        if(obj_assetSwitchList != null)
        {
            obj_assetSwitchList.Sort(CompareGameObjectNames);
        }
    }

    //Sort by names
    private int CompareGameObjectNames(GameObject a, GameObject b)
    {
        return a.name.CompareTo(b.name);
    }

    public void deactivateObjSwitch()
    {
        foreach(GameObject obj in obj_assetSwitchList)
        {
            obj.SetActive(false);
        }
    }

    public void activateObjSwitch()
    {
        foreach (GameObject obj in obj_assetSwitchList)
        {
            obj.SetActive(true);
        }
    }
    public void ChangeObjectMaterial(GameObject target, Color color)
    {
        if(target != null)
        {
            Renderer renderer = target.GetComponent<Renderer>();

            if (renderer != null)
            {
                Material myMaterial = renderer.material;
                myMaterial.color = color;
            }
            else
            {
                Debug.LogError("Renderer component not found on the GameObject.");
            }
        }
        
    }

}
