using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class objCondition : MonoBehaviour
{
    public  scriptableObject script_Scriptable;
    private singletonData script_Data;
    private waitTimer script_UI_WaitTimer;
    private taskUI script_TaskUI;
    private List<levelData> list;

    //Show if errors
    private GameObject eleOnObj;
    public List<GameObject> objList;
    [HideInInspector] public bool done;
    private void Awake()
    {
        script_UI_WaitTimer = GetComponent<waitTimer>();
        script_Data = GetComponent<singletonData>();
        script_TaskUI = GetComponent<taskUI>();
        list = script_Scriptable.dataList;
        eleOnObj = GameObject.FindWithTag("Electricity");
        
    }

    private void Update()
    {
        loseCheck();
        eleSwitch();

        if (script_Scriptable.eleCapacity <= 0)
        {
            //Debug.Log("Elec Out");
        }
        else
        {
            script_Scriptable.ele = true;
        }

        if (GameObject.FindWithTag("Electricity") != null  && !done)
        {
            //Debug.Log("Find Electricity & Obj");
            eleOnObj = GameObject.FindWithTag("Electricity");

            done = true;
        }

    }
   
    public void objColor()
    {
        //Debug.Log("Color Updated");
        for (int i = 0; i < objList.Count; i++)
        {
            if(objList != null)
            {
                GameObject obj = objList[i];
                if (list[i].eleOn && !list[i].taskActive)
                {
                    ChangeObjectMaterial(obj, Color.yellow);
                }

                if (list[i].taskActive)
                {
                    ChangeObjectMaterial(obj, Color.green);

                    if (list[i].taskActive && list[i].taskTimer <= 0)
                    {
                        ChangeObjectMaterial(obj, Color.blue);
                    }
                }

                if (!list[i].eleOn)
                {
                    ChangeObjectMaterial(obj, Color.white);
                }
            }
        }
    }

    public void objSwitch(GameObject target)
    {
        //Debug.Log("Here");
        for (int i = 0; i < list.Count; i++)
        {
            if(target.name == objList[i].name)
            {
                //Debug.Log($"Activated Found of index {i}");
                //Debug.Log("Object Activated");
                
                if (!list[i].taskActive)
                {
                    list[i].taskActive = !list[i].taskActive;
                    script_Scriptable.eleCapacity -= list[i].wattage;
                    if (script_Scriptable.eleCapacity < 0)
                    {
                        Debug.Log("Electricity Out");
                        elecOut();
                        objColor();
                        break;
                    }
                    
                    else
                    {
                        StartCoroutine(timerDecreasePerSec(i));
                        script_UI_WaitTimer.StartTimer(list[i].taskTimer, list[i].taskName, i);
                        objColor();
                        break;
                    }
                }
                else
                {
                    
                    list[i].taskActive = !list[i].taskActive;
                    script_Scriptable.eleCapacity += list[i].wattage;
                    //Debug.Log("Object Deactivated");
                }
            }
        }
    }

    public void elecOut()
    {
        for (int i = 0; i < list.Count; i++)
        {
            list[i].eleOn = false;
            list[i].taskActive = false;
            script_Scriptable.ele = false;
        }
    }

    public void eleSwitch()
    {
        if (eleOnObj != null)
        {
            GameObject obj = eleOnObj;
            Collider collider = obj.GetComponent<Collider>();
            if (script_Scriptable.ele)
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

    public void eleButton()
    {
        
        if(!script_Scriptable.ele)
        {
            Debug.Log("Elec On");
            for (int i = 0; i < list.Count; i++)
            {
                list[i].eleOn = true;
                script_Scriptable.eleCapacity += list[i].wattage;
                objColor();
                if (list[i].taskRestore)
                {
                    list[i].taskActive = true;
                    script_Scriptable.eleCapacity -= list[i].wattage;
                    objColor();
                }
            }
        }
    }

    public void winCheck()
    {
        List<bool> check = new List<bool>();
        for (int i = 0; i < list.Count; i++)
        {
            check.Add(list[i].taskStats);
        }

        if (!check.Contains(false))
        {
            Debug.Log("You Win!");
            script_Data.win = true;
        }
    }

    public void loseCheck()
    {
        if(script_Scriptable.timer <= 0 || script_Scriptable.eleQuota <= 0)
        {
            script_Data.lose = true;
        }
    }

    IEnumerator eleDecreasePerSec(int index,float amount)
    {
        while (true && list[index].taskActive)
        {
            script_Scriptable.eleQuota -= amount * Time.deltaTime;
            if(list[index].taskTimer < 0)
            {
                yield break;
            }
            yield return null; // Wait for the next frame
        }
    }

    IEnumerator timerDecreasePerSec(int index)
    {
        if (index < 0 || index >= list.Count)
        {
            //Debug.LogError("Invalid index provided for timerDecreasePerSec coroutine.");
            yield break; // Exit the coroutine if the index is out of range
        }

        StartCoroutine(eleDecreasePerSec(index,list[index].wattPerSec));

        while (list[index].taskTimer >= 0 && list[index].taskActive)
        {
            list[index].taskTimer -= Time.deltaTime;
            if (list[index].taskTimer <= 0)
            {
                list[index].taskStats = !list[index].taskStats;
                script_TaskUI.updateTask();
                objColor();
                //Debug.Log("Object Deactivated-Done Timer");
                winCheck();
                yield break; // Exit the coroutine if the taskTimer has reached zero
            }
            yield return null; // Wait for the next frame
        }
    }

    public void assignObj()
    {
        if(objList != null)
        {
            objList.Clear();
        }
        
        foreach (GameObject x in GameObject.FindGameObjectsWithTag("Switch"))
        {
            objList.Add(x);
            //Debug.Log("Herre");
        }
        if(objList != null)
        {
            objList.Sort(CompareGameObjectNames);
        }
        
        //Debug.Log("Object Assigned");
    }

    private int CompareGameObjectNames(GameObject a, GameObject b)
    {
        return a.name.CompareTo(b.name);
    }

    public void deactivateObj()
    {
        foreach(GameObject obj in objList)
        {
            obj.SetActive(false);
        }
    }

    public void activateObj()
    {
        foreach (GameObject obj in objList)
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
        // Call your method here
        assignObj();
        objColor();
    }
}
