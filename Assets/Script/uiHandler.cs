using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class uiHandler : MonoBehaviour
{
    public scriptableObject script_Scriptable;
    private cameraState script_cameraState;
    private touchCode script_touchCode;

    private Canvas canvasMenu;
    private TMP_Text textUI;
    private TMP_Text textWatt;
    private TMP_Text textQuota;
    public void Awake()
    {
        script_cameraState = GetComponent<cameraState>();
        script_touchCode = GetComponent<touchCode>();
    }

    private void Start()
    {
        canvasMenu = GameObject.Find("Canvas_Menu").GetComponent<Canvas>();
        textUI = GameObject.Find("Timer").GetComponent<TextMeshProUGUI>();
        textWatt = GameObject.Find("EleCapacity").GetComponent<TextMeshProUGUI>();
        textQuota = GameObject.Find("EleQuota").GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        setTimer();
        updateWattageUI();
        updateQuotaUI();
    }


    public void backButton()
    {
        if (!script_cameraState.currentVirtualCamera.CompareTag("mainVirtualCamera"))
        {
            script_cameraState.activatePrevCamera();
            script_touchCode.deactivateObj();
            script_touchCode.activateSecLevel();
        }
        else
        {
            Time.timeScale = 0;
            canvasMenu.enabled = true;
        }
    }

    public void updateTimerUI(TMP_Text textCanvas)
    {
        string minutes = Mathf.Floor(script_Scriptable.timer / 60).ToString("00");
        string seconds = (script_Scriptable.timer % 60).ToString("00");

        textCanvas.text = "Time : " + minutes + ":" + seconds;
    }

    void setTimer()
    {
        script_Scriptable.timer -= Time.deltaTime;

        updateTimerUI(textUI);

        if (script_Scriptable.timer <= 0)
        {
            Debug.Log("Timer reached zero!");
        }
    }

    public void updateWattageUI()
    {
        textWatt.text = "Capacity : " + script_Scriptable.eleCapacity;
    }

    public void updateQuotaUI()
    {
        textQuota.text = "Quota : " + script_Scriptable.eleQuota;
    }
}
