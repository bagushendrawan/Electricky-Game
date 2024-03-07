using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class waitTimer : MonoBehaviour
{
    private TMP_Text timerText;
    public scriptableObject script_scriptable;
    public Dictionary<int, Timer> timers = new Dictionary<int, Timer>();

    // Start is called before the first frame update
    void Start()
    {
        timerText = GameObject.Find("ObjTimer").GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        // Update and display timers
        UpdateTimers();
    }

    // Start a new timer
    public void StartTimer(float duration, string nameObj, int index)
    {
        if (!timers.ContainsKey(index))
        {
            Timer newTimer = new Timer(duration);
            timers.Add(index, newTimer);
        }
    }

    // Update and display all timers
    void UpdateTimers()
    {
        // Remove finished timers
        List<int> keysToRemove = new List<int>();
        foreach (var pair in timers)
        {
            int index = pair.Key;
            Timer timer = pair.Value;
            if (timer.IsFinished || !script_scriptable.dataList[index].taskActive)
            {
                keysToRemove.Add(index);
            }
        }

        foreach (int index in keysToRemove)
        {
            timers.Remove(index);
        }

        // Display active timers in UI
        DisplayTimers();

        // Update each timer
        foreach (var pair in timers)
        {
            pair.Value.UpdateTimer(Time.deltaTime);
        }
    }

    // Display active timers in UI
    void DisplayTimers()
    {
        if (timerText != null)
        {
            // Clear existing text
            timerText.text = "";

            // Display each active timer
            foreach (var pair in timers)
            {
                int index = pair.Key;
                string nameObj = script_scriptable.dataList[index].taskName;
                Timer timer = pair.Value;
                timerText.text += $"{nameObj} Timer: {timer.Duration - timer.ElapsedTime:F1}s\n";
            }
        }
    }
}

// Timer class to track elapsed time
public class Timer
{
    public float Duration { get; private set; }
    public float ElapsedTime { get; private set; }
    public bool IsFinished => ElapsedTime >= Duration;

    public Timer(float duration)
    {
        Duration = duration;
        ElapsedTime = 0f;
    }

    public void UpdateTimer(float deltaTime)
    {
        if (!IsFinished)
        {
            ElapsedTime += deltaTime;
        }
    }
}
