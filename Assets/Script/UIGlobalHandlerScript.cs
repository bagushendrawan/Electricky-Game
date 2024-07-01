using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIGlobalHandlerScript : MonoBehaviour
{
    public ScriptableObjectScript script_scriptable;
    private FSMCameraRoomScript script_cameraState;
    private TouchCodeScript script_touchCode;
    [SerializeField] private CheckACValueScript script_acObj;

    public Canvas canvasMenu;
    public TMP_Text textUI;
    public TMP_Text textWatt;
    public TMP_Text textQuota;
    public TMP_Text textFPS;
    public Image timerBar;
    public Image capacityBar;
    public Image quotaBar;
    public GameObject buttonBack;
    private float avgFrameRate;

    private void Start()
    {
        script_cameraState = GetComponent<FSMCameraRoomScript>();
        script_touchCode = GetComponent<TouchCodeScript>();
        //canvasMenu = GameObject.Find("Canvas_Menu").GetComponent<Canvas>();
        //textUI = GameObject.Find("Timer").GetComponent<TextMeshProUGUI>();
        //textWatt = GameObject.Find("EleCapacity").GetComponent<TextMeshProUGUI>();
        //textQuota = GameObject.Find("EleQuota").GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        globalTimer();
        updateGlobalWattageUI();
        updateGlobalQuotaUI();
        avgFrameRate = Time.frameCount / Time.time;
        textFPS.text = "FPS : " + avgFrameRate.ToString("00");
        if (script_cameraState.currentVirtualCamera.CompareTag("mainVirtualCamera"))
        {
            buttonBack.SetActive(false);
        } else
        {
            buttonBack.SetActive(true);
        }
    }

    //Back button switch
    public void backButton()
    {
        if (!script_cameraState.currentVirtualCamera.CompareTag("mainVirtualCamera"))
        {
            script_cameraState.activatePrevCamera();
            if(ObjConditionScript.script_checkAC != null && ObjConditionScript.script_checkAC.isThisTV)
            {
                ObjConditionScript.script_checkAC.deactivateACCollider();
            }

            if (ObjConditionScript.script_checkAC != null && !ObjConditionScript.script_checkAC.isThisTV)
            {
                ObjConditionScript.script_checkAC.deactivateACCollider();
            }

            script_touchCode.deactiveSecCollider();
        }
        //else
        //{
        //    Time.timeScale = 0;
        //    canvasMenu.enabled = true;
        //}
    }

    public void pauseButton()
    {
        Time.timeScale = 0;
        canvasMenu.enabled = true;
    }

    public void updateGlobalTimerUI(TMP_Text textCanvas)
    {
        string minutes = Mathf.Floor(script_scriptable.global_timer / 60).ToString("00");
        string seconds = (script_scriptable.global_timer % 60).ToString("00");

        textCanvas.text = minutes + ":" + seconds;
    }

    void globalTimer()
    {
        timerBar.fillAmount = script_scriptable.global_timer / SingletonDataScript.timer;
        script_scriptable.global_timer -= Time.deltaTime;
        updateGlobalTimerUI(textUI);

        if (script_scriptable.global_timer <= 0)
        {
            Debug.Log("Timer reached zero!");
        }
    }

    public void updateGlobalWattageUI()
    {
        //Debug.Log("Percent Capacity " + script_scriptable.global_eleCapacity / SingletonDataScript.eleCapacity + " " + SingletonDataScript.eleCapacity + " " + script_scriptable.global_eleCapacity);
        float capacityDiff = script_scriptable.global_eleCapacity / SingletonDataScript.eleCapacity;
        capacityBar.fillAmount = capacityDiff;
        textWatt.text = script_scriptable.global_eleCapacity.ToString("0");
        if(!script_scriptable.global_eleOn_Q)
        {
            textWatt.text = "Out!";
        }
    }

    public void updateGlobalQuotaUI()
    {
        quotaBar.fillAmount = script_scriptable.global_eleQuota / SingletonDataScript.eleQuota;
        textQuota.text = Mathf.Round(script_scriptable.global_eleQuota).ToString("0");
    }
}
