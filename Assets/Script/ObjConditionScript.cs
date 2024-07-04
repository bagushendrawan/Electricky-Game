using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ObjConditionScript : MonoBehaviour
{
    [SerializeField] private ScriptableObjectScript script_scriptable;
    private SingletonDataScript script_singletonData;
    [HideInInspector] public TaskWaitTimerUIScript script_waitTimer;
    private TaskStatsUIScript script_taskUi;
    public BedLampBehaviourScript script_bedlamp;
    public static CheckACValueScript script_checkAC;
    public Texture2D defaultTVTexture;

    public AudioSource audioSource;
    public AudioClip elecOutClip;
    public Animator elec_animator;
    public Canvas elec_canvas;
    public CheckACValueScript script_tv;

    //Get global tronic data list and store it here
    [HideInInspector] public static List<LevelDataClass> obj_dataList;
    [SerializeField] public static List<GameObject> obj_roomList;

    //Show if errors
    [Header("Assign This for each level")]
    public GameObject global_electricitySwitch;
    public List<GameObject> obj_assetSwitchList;
    public List<GameObject> room_list;
    public List<CheckACValueScript> acTVScript;

    public static Dictionary<int, acObjBehaviour> global_acStatsIndex = new();

    public enum acObjBehaviour
    {
        off,
        initialized,
        activated,
        correct
    }
    private void Start()
    {
        script_waitTimer = GetComponent<TaskWaitTimerUIScript>();
        script_singletonData = GetComponent<SingletonDataScript>();
        script_taskUi = GetComponent<TaskStatsUIScript>();
        obj_dataList = script_scriptable.global_tronicDataList;
        obj_roomList = room_list;
    }

    private void Update()
    {
        loseCheck();
        eleSwitch();
    }
   
    //update all asset color inside obj_assetList
    public void objColor()
    {
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
        for (int j = 0; j < obj_dataList.Count; j++)
        {
            if(target.name == obj_assetSwitchList[j].name)
            {

                if (!obj_dataList[j].tronic_active_Q && obj_dataList[j].tronic_eleSupplied_Q)
                {
                    obj_dataList[j].tronic_active_Q = true;
                    script_bedlamp.bedroomLampCheck();
                    StartCoroutine(eleDecreasePerSec(j, obj_dataList[j].tronic_wattPerSec));
                    obj_dataList[j].tronic_timerCoroutine = StartCoroutine(timerDecreasePerSec
                            (j));
                    script_waitTimer.StartTimer(obj_dataList[j].tronic_timer, j, false);
                    script_taskUi.updateTask();
                }
                else
                {   
                    obj_dataList[j].tronic_active_Q = false;
                    script_bedlamp.bedroomLampCheck();
                    StartCoroutine(eleDecreasePerSec(j, obj_dataList[j].tronic_wattPerSec));
                    StartCoroutine(timerDecreasePerSec
                            (script_bedlamp.bedlampIndex));
                    script_waitTimer.StartTimer(obj_dataList[script_bedlamp.bedlampIndex].tronic_timer, script_bedlamp.bedlampIndex, false);
                    script_taskUi.updateTask();
                }
            }
        }
    }

    public void objACSwitch(GameObject target)
    {
        for (int j = 0; j < obj_dataList.Count; j++)
        {
            if (target.name == obj_assetSwitchList[j].name)
            {
                if (!obj_dataList[j].tronic_active_Q && obj_dataList[j].tronic_eleSupplied_Q)
                {
                    obj_dataList[j].tronic_active_Q = true;
                    //script_scriptable.global_eleCapacity -= obj_dataList[j].tronic_wattage;


                    if (global_acStatsIndex[script_checkAC.acIndex] != acObjBehaviour.correct && !script_checkAC.isThisTV)
                    {
                        script_checkAC.ChangeState(CheckACValueScript.objBehaviour.activated);
                        global_acStatsIndex[script_checkAC.acIndex] = acObjBehaviour.activated;
                    }
                    script_checkAC.ACButtonPressed(true);
                        script_checkAC.acIndex = j;
                        StartCoroutine(eleACDecreasePerSec(script_checkAC.acIndex, obj_dataList[script_checkAC.acIndex].tronic_wattPerSec));
                        objACStats();
                        script_taskUi.updateTask();
                    //objColor();
                }
                else
                {
                    //AC Anim Off if being pressed off
                    script_checkAC.ACButtonPressed(false);
                    obj_dataList[j].tronic_active_Q = false;
                    objACStats();
                    script_scriptable.global_eleCapacity += obj_dataList[script_checkAC.acIndex].tronic_wattage;
                    script_taskUi.updateTask();
                    //script_scriptable.global_eleCapacity += obj_dataList[j].tronic_wattage;
                }
            }
        }
    }

    public void consoleCheck()
    {
        if (script_tv.isTVAV && ObjConditionScript.obj_dataList[script_tv.tvIndex].tronic_active_Q && ObjConditionScript.obj_dataList[script_tv.consoleIndex].tronic_active_Q)
        {
            ObjConditionScript.obj_dataList[script_tv.consoleIndex].tronic_correct_Q = true;
            script_tv.rendererAC.material.SetTexture("_MainTex", script_tv.textureACList[4]);
        }
        else
        {
            //script_ac.rendererAC.material.SetTexture("_MainTex", null);
            ObjConditionScript.obj_dataList[script_tv.consoleIndex].tronic_correct_Q = false;
        }


        if (ObjConditionScript.obj_dataList[script_tv.consoleIndex].tronic_correct_Q)
        {
            StartCoroutine(timerDecreasePerSec(script_tv.consoleIndex));
            script_waitTimer.StartTimer(ObjConditionScript.obj_dataList[script_tv.consoleIndex].tronic_timer, script_tv.consoleIndex, false);
        }
    }

    public void objACStats()
    {
        if(script_checkAC != null)
        {
            if (script_checkAC.defValue == script_checkAC.valueCondition)
            {
                //Debug.Log("AC CORRECT" + obj_dataList[script_checkAC.acIndex].tronic_timer);
                script_checkAC.ChangeState(CheckACValueScript.objBehaviour.correct);
                global_acStatsIndex[script_checkAC.acIndex] = acObjBehaviour.correct;
                obj_dataList[script_checkAC.acIndex].tronic_correct_Q = true;
                obj_dataList[script_checkAC.acIndex].tronic_timerCoroutine = StartCoroutine(timerACDecreasePerSec(script_checkAC.acIndex));
                script_waitTimer.StartTimer(obj_dataList[script_checkAC.acIndex].tronic_timer, script_checkAC.acIndex, true);
            }
            else
            {
                obj_dataList[script_checkAC.acIndex].tronic_correct_Q = false;
            }
        }
    }


    //turn off electricity
    public void elecOut()
    {
        script_scriptable.global_eleOn_Q = false;
        audioSource.PlayOneShot(elecOutClip);
        elec_canvas.enabled = true;

        for (int i = 0; i < obj_dataList.Count; i++)
        {
            obj_dataList[i].tronic_eleSupplied_Q = false;
            obj_dataList[i].tronic_active_Q = false;
        }

        for (int j = 0; j < acTVScript.Count; j++)
        {
            acTVScript[j].ChangeState(CheckACValueScript.objBehaviour.off);
            if (acTVScript[j].isThisTV)
            {
                acTVScript[j].rendererAC.material.SetTexture("_MainTex", defaultTVTexture);
            }
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
                //ChangeObjectMaterial(obj, Color.yellow);
                collider.enabled = false;
            }
        }
    }

    //bring back electricity
    public void eleButton()
    {
        
        if(!script_scriptable.global_eleOn_Q)
        {
            script_scriptable.global_eleOn_Q = true;
            script_scriptable.global_eleCapacity = SingletonDataScript.eleCapacity;
            elec_animator.SetBool("on", true);
            for (int i = 0; i < obj_dataList.Count; i++)
            {
                //script_bedlamp.bedroomLampCheck();
                obj_dataList[i].tronic_eleSupplied_Q = true;
               
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
            for (int i = 0; i < obj_dataList.Count; i++)
            {
                obj_dataList[i].tronic_active_Q = false;
            }
            Debug.Log("You Win!");
            script_singletonData.global_win_Q = true;
            dataHandler.unlockedScene++;
        }
    }

    public void loseCheck()
    {
        if(script_scriptable.global_timer <= 0 || script_scriptable.global_eleQuota <= 0)
        {
            script_singletonData.global_lose_Q = true;
            for (int i = 0; i < obj_dataList.Count; i++)
            {
                obj_dataList[i].tronic_active_Q = false;
            }
        }
    }

   public IEnumerator eleDecreasePerSec(int index,float amount)
    {
        if(true && obj_dataList[index].tronic_active_Q && obj_dataList[index].tronic_eleSupplied_Q)
        {
            script_scriptable.global_eleCapacity -= obj_dataList[index].tronic_wattage;
            if (script_scriptable.global_eleCapacity < 0)
            {
                Debug.Log("Electricity Out");
                elecOut();
                //objColor();
            }
            while (true && obj_dataList[index].tronic_active_Q && obj_dataList[index].tronic_eleSupplied_Q)
            {
                script_scriptable.global_eleQuota -= amount * Time.deltaTime;

                if (script_scriptable.global_eleQuota < 0)
                {
                    yield break;
                }
                yield return null;
            }
        } else
        {
            script_scriptable.global_eleCapacity += obj_dataList[index].tronic_wattage;
        }
    }

    //Decrease the timer and update the task
    public IEnumerator timerDecreasePerSec(int index)
    {
        if (index < 0 || index >= obj_dataList.Count)
        {
            Debug.LogError("Invalid index provided for timerDecreasePerSec coroutine.");
            yield break; 
        }


        
        while (obj_dataList[index].tronic_timer >= 0 && obj_dataList[index].tronic_active_Q && obj_dataList[index].tronic_correct_Q && obj_dataList[index].tronic_eleSupplied_Q)
        {
            obj_dataList[index].tronic_timer -= Time.deltaTime;

            if (obj_dataList[index].tronic_timer <= 0)
            {
                obj_dataList[index].tronic_statsDone_Q = true;
                script_taskUi.updateTask();
                winCheck();
                yield break; 
            }
            yield return null; 
        }
    }

    IEnumerator eleACDecreasePerSec(int index, float amount)
    {
        if(script_checkAC.state_acBehaviour == CheckACValueScript.objBehaviour.activated || script_checkAC.state_acBehaviour == CheckACValueScript.objBehaviour.correct)
        {
            script_checkAC.isACActive = true;
            if (true && obj_dataList[index].tronic_active_Q && obj_dataList[index].tronic_eleSupplied_Q && script_checkAC.isACActive)
            {
                script_scriptable.global_eleCapacity -= obj_dataList[index].tronic_wattage;
                if (script_scriptable.global_eleCapacity < 0)
                {
                    Debug.Log("Electricity Out");
                    elecOut();
                }
                while (true && obj_dataList[index].tronic_active_Q && obj_dataList[index].tronic_eleSupplied_Q)
                {
                    script_scriptable.global_eleQuota -= amount * Time.deltaTime;

                    if (script_scriptable.global_eleQuota < 0)
                    {
                        yield break;
                    }
                    yield return null;
                }
            }
        }
        
    }

    IEnumerator timerACDecreasePerSec(int index)
    {
        if (index < 0 || index >= obj_dataList.Count)
        {
            Debug.LogError("Invalid index provided for timerDecreasePerSec coroutine.");
            yield break;
        }

        while (obj_dataList[index].tronic_timer >= 0 && obj_dataList[index].tronic_active_Q && global_acStatsIndex[index] == acObjBehaviour.correct && obj_dataList[index].tronic_correct_Q && obj_dataList[index].tronic_eleSupplied_Q)//errr
        {
            obj_dataList[index].tronic_timer -= Time.deltaTime;
            if (obj_dataList[index].tronic_timer <= 0)
            {
                obj_dataList[index].tronic_statsDone_Q = !obj_dataList[index].tronic_statsDone_Q;
                script_taskUi.updateTask();

                winCheck();

                yield break; 
            }
            yield return null;
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
