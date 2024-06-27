using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

//Show task Timer Stack in UI
public class TaskWaitTimerUIScript : MonoBehaviour
{
    //private TMP_Text timerText;
    public ScriptableObjectScript script_scriptable;
    public ObjConditionScript script_objCon;
    public Dictionary<int, Timer> global_taskTimerActive = new Dictionary<int, Timer>();
    public Dictionary<int, Timer> global_acTimerActive = new Dictionary<int, Timer>();

    public GameObject timerPrefabs;
    public Transform prefabsParent;
    public Vector3 positionOffset;
    public List<Texture2D> objSpriteIcon = new();
    public GameObject[] prefabsArray;
    public float[] durationArray;
    
    // Start is called before the first frame update
    void Start()
    {
        //Find objTimer in canvas
        //timerText = GameObject.Find("ObjTimer").GetComponent<TMP_Text>();
        initializedPrefabs();
    }

    // Update is called once per frame
    void Update()
    {
        // Update and display timers
        UpdateTimers();

        // Display active timers in UI
        DisplayTimers();
    }

    // Start a new timer
    public void StartTimer(float duration, int index, bool isAC)
    {
        if(isAC)
        {
            if (!global_acTimerActive.ContainsKey(index))
            {
                Debug.Log("AC START TIMER UI" + index);
                Timer newTimer = new Timer(duration, durationArray[index]);
                global_acTimerActive.Add(index, newTimer);
            } else
            {
                Timer newTimer = new Timer(duration, durationArray[index]);
                global_acTimerActive[index] = newTimer;
            }
        } else
        {
            if (!global_taskTimerActive.ContainsKey(index))
            {
                Debug.Log("NOT CONTAINS KEY");
                Timer newTimer = new Timer(duration, durationArray[index]);
                global_taskTimerActive.Add(index, newTimer);
            } else
            {
                Debug.Log("CONTAINS KEY");
                Timer newTimer = new Timer(duration, durationArray[index]);
                global_taskTimerActive[index] = newTimer;
            }
        }
        
    }

    void initializedPrefabs()
    {
        prefabsArray = new GameObject[objSpriteIcon.Count];
        durationArray = new float[objSpriteIcon.Count];
        for(int j =0; j < objSpriteIcon.Count; j++)
        {
            durationArray[j] = script_scriptable.global_tronicDataList[j].tronic_timer;
        }

        for(int i = 0; i<objSpriteIcon.Count; i++)
        {
            //instantiate the image here
            prefabsArray[i] = Instantiate(timerPrefabs, prefabsParent);

            RawImage objImg = prefabsArray[i].GetComponent<RawImage>();

            if (objImg != null)
            {
                objImg.texture = objSpriteIcon[i];
            }

            prefabsArray[i].SetActive(false);
        }
    }

    // Update and display all timers
    void UpdateTimers()
    {
        // Remove finished timers
        List<int> keysToRemove = new List<int>();
        List<int> acKeysToRemove = new List<int>();
        foreach (var pair in global_taskTimerActive)
        {
            int index = pair.Key;
            Timer timer = pair.Value;
            
            if (timer.IsFinished || !script_scriptable.global_tronicDataList[index].tronic_active_Q || !script_scriptable.global_tronicDataList[index].tronic_correct_Q)
            {
                    keysToRemove.Add(index);
            }
        }

        foreach (var pair in global_acTimerActive)
        {
            int index = pair.Key;
            Timer timer = pair.Value;

            if (timer.IsFinished || !script_scriptable.global_tronicDataList[index].tronic_active_Q ||  ObjConditionScript.global_acStatsIndex[index] != ObjConditionScript.acObjBehaviour.correct || !script_scriptable.global_tronicDataList[index].tronic_correct_Q)
            {
                Debug.Log("ackey removed");
                acKeysToRemove.Add(index);
            }
        }


        foreach (int index in keysToRemove)
        {
            global_taskTimerActive.Remove(index);
            prefabsArray[index].SetActive(false);
        }

        foreach (int index in acKeysToRemove)
        {
            Debug.Log("acremove count " + acKeysToRemove.Count);
            global_acTimerActive.Remove(index);
            prefabsArray[index].SetActive(false);
        }

        acKeysToRemove.Clear();
        // Update each timer
        foreach (var pair in global_taskTimerActive)
        {
            pair.Value.UpdateTimer(Time.deltaTime);
        }

        foreach (var pair in global_acTimerActive)
        {
            pair.Value.UpdateTimer(Time.deltaTime);
        }
    }

    // Display active timers in UI
    void DisplayTimers()
    {
            int i = 0;
            
            // Display each active timer
            foreach (var pair in global_taskTimerActive)
            {
                
                int index = pair.Key;
                string nameObj = script_scriptable.global_tronicDataList[index].tronic_name;
                Timer timer = pair.Value;

                // Calculate the new position
                Vector3 newPosition = positionOffset * i++;

                // Set the position
                RectTransform rectTransform = prefabsArray[index].GetComponent<RectTransform>();
                if (rectTransform != null)
                {
                    rectTransform.anchoredPosition = newPosition;
                }

                prefabsArray[index].SetActive(true);
                Image objImgCircle = prefabsArray[index].GetComponentInChildren<Image>();

                if (objImgCircle != null)
                {
                    //objImg.sprite = objSpriteIcon[index];
                    //Debug.Log("fill AMOUNT");
                    objImgCircle.fillAmount = timer.FillAmount;
                }

                //timerText.text += $"{nameObj} Timer: {timer.Duration - timer.ElapsedTime:F1}s\n";
            }

            foreach (var pair in global_acTimerActive)
            {
                int index = pair.Key;
                string nameObj = script_scriptable.global_tronicDataList[index].tronic_name;
                Timer timer = pair.Value;

                // Calculate the new position
                Vector3 newPosition = positionOffset * i++;

                // Set the position
                RectTransform rectTransform = prefabsArray[index].GetComponent<RectTransform>();
                if (rectTransform != null)
                {
                    rectTransform.anchoredPosition = newPosition;
                }

                Debug.Log("prefabs ac ui active " + index);
                prefabsArray[index].SetActive(true);
                Image objImg = prefabsArray[index].GetComponentInChildren<Image>();

                if (objImg != null)
                {
                    //objImg.sprite = objSpriteIcon[index];
                    objImg.fillAmount = timer.FillAmount;
                }
                //timerText.text += $"{nameObj} Timer: {timer.Duration - timer.ElapsedTime:F1}s\n";
            }
        }
}

// Timer class to track elapsed time
public class Timer
{
    public float Duration { get; private set; } 
    public float ElapsedTime { get; private set; }
    public float RemainingTime { get; private set; }
    public float FillAmount { get; private set; }
    public bool IsFinished => ElapsedTime >= Duration;

    public Timer(float duration, float originDuration)
    {
        Duration = duration;
        ElapsedTime = originDuration - duration;
    }

    public void UpdateTimer(float deltaTime)
    {
        if (!IsFinished)
        {
            ElapsedTime += deltaTime;
            RemainingTime = Mathf.Clamp(Duration - ElapsedTime, 0, Duration);
            FillAmount = RemainingTime / Duration;
        }
    }
}
