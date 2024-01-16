using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class uiScript : MonoBehaviour
{
    private camera script_camera;
    private singletonStats script_stats;
    public GameObject canvasMenu;

    public TMP_Text textUI;
    public TMP_Text textWatt;

    private void Start()
    {
        script_camera = GetComponent<camera>();
        script_stats = GetComponent<singletonStats>();
    }

    public void backButton()
    {
        if (Camera.main.name != "Camera_main")
        {
            script_camera.deactivateAllCamera();
            GameObject objectToActivate = script_camera.prevCamera;
            if (objectToActivate != null)
            {
                objectToActivate.SetActive(true);
            }
            else
            {
                Debug.Log("No prev camera");
            }
        }
        else
        {
            Time.timeScale = 0;
            canvasMenu.SetActive(true);
        }
    }

    public void resumeButton()
    {
        Time.timeScale = 1;
        canvasMenu.SetActive(false);
    }

    public void updateTimerUI()
    {
        // Format the time as minutes and seconds
        string minutes = Mathf.Floor(script_stats.timer / 60).ToString("00");
        string seconds = (script_stats.timer % 60).ToString("00");

        // Update the UI Text component with the formatted time
        textUI.text = "Time : " + minutes + ":" + seconds;
    }

    public void updateWattageUI()
    {
        // Update the UI Text component with the formatted time
        textWatt.text = "Capacity : " + script_stats.eleCapacity;
    }
}

