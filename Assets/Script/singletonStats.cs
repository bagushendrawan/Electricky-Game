using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class singletonStats : MonoBehaviour
{
    private static singletonStats _instance;
    public scriptableObject scriptableScript;
    private uiScript script_ui;
    public float totalTime;
    public float timer { get; private set; }
    public int eleCapacity;

    public static singletonStats Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<singletonStats>();

                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject("GameManager");
                    _instance = singletonObject.AddComponent<singletonStats>();
                }
            }

            return _instance;
        }
    }
    
    void setTimer()
    {
        // Update the timer
        timer -= Time.deltaTime;

        // Update the UI Text component
        script_ui.updateTimerUI();

        // Check if the timer has reached zero
        if (timer <= 0)
        {
            // Perform actions when the timer reaches zero (e.g., game over)
            Debug.Log("Timer reached zero!");
            // You might want to add logic to handle what happens when the timer reaches zero.
        }
    }

   
    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void Start()
    {
        timer = totalTime;
        script_ui = GetComponent<uiScript>();
    }

    private void Update()
    {
        setTimer();
        script_ui.updateWattageUI();
    }
}
