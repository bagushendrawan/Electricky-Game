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

    private Canvas canvasMenu;
    private TMP_Text textUI;
    private TMP_Text textWatt;
    private TMP_Text textQuota;

    private void Start()
    {
        script_cameraState = GetComponent<FSMCameraRoomScript>();
        script_touchCode = GetComponent<TouchCodeScript>();
        canvasMenu = GameObject.Find("Canvas_Menu").GetComponent<Canvas>();
        textUI = GameObject.Find("Timer").GetComponent<TextMeshProUGUI>();
        textWatt = GameObject.Find("EleCapacity").GetComponent<TextMeshProUGUI>();
        textQuota = GameObject.Find("EleQuota").GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        globalTimer();
        updateGlobalWattageUI();
        updateGlobalQuotaUI();
    }

    //Back button switch
    public void backButton()
    {
        if (!script_cameraState.currentVirtualCamera.CompareTag("mainVirtualCamera"))
        {
            script_cameraState.activatePrevCamera();
            if(ObjConditionScript.script_checkAC != null)
            {
                ObjConditionScript.script_checkAC.deactivateACCollider();
            }

            script_touchCode.deactiveSecCollider();
        }
        else
        {
            Time.timeScale = 0;
            canvasMenu.enabled = true;
        }
    }

    public void updateGlobalTimerUI(TMP_Text textCanvas)
    {
        string minutes = Mathf.Floor(script_scriptable.global_timer / 60).ToString("00");
        string seconds = (script_scriptable.global_timer % 60).ToString("00");

        textCanvas.text = "Time : " + minutes + ":" + seconds;
    }

    void globalTimer()
    {
        script_scriptable.global_timer -= Time.deltaTime;

        updateGlobalTimerUI(textUI);

        if (script_scriptable.global_timer <= 0)
        {
            Debug.Log("Timer reached zero!");
        }
    }

    public void updateGlobalWattageUI()
    {
        textWatt.text = "Capacity : " + script_scriptable.global_eleCapacity;
    }

    public void updateGlobalQuotaUI()
    {
        textQuota.text = "Quota : " + Mathf.Round(script_scriptable.global_eleQuota);
    }
}
