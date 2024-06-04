using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CheckACValueScript : MonoBehaviour
{
    [HideInInspector] public int acStatsIndex;
    public bool isTVAV;
    [SerializeField] public int consoleIndex;
    [SerializeField] public int tvIndex;
    [SerializeField] public bool isThisTV = false;

    public List<Texture2D> textureTVRemote = new();
    [SerializeField] public Renderer remoteTVRenderer;

    private bool objActive = false;
    public int defValue;
    public int changeVal;
    public int valueCondition;
    public GameObject objToActivate;
    public GameObject objToDeactivate;

    public List<Texture2D> textureACList = new();
    public List<Texture2D> emissionACList = new();
    public Renderer rendererRemote;
    public Renderer rendererAC;
    [SerializeField] private GameObject ac_remoteSwitch;
    [SerializeField] private Collider console_switch;
    [SerializeField] private Animator acAnimator;
    [SerializeField] private ObjConditionScript script_obj;
    [SerializeField] private Light acLight;
    [SerializeField] private Light remoteLight;

    [SerializeField] private GameObject remoteScreenCover;
    [SerializeField] private GameObject acScreenCover;
    [HideInInspector] public int acIndex;
    [HideInInspector] public bool isACActive = false;

    // Update is called once per frame
    void Update()
    {
        //valueCheck(valueCondition);
        PerformStateBehaviour();
    }

    public enum objBehaviour
    {
        off,
        initialized,
        activated,
        correct
    }

    public objBehaviour state_acBehaviour;

    public void ChangeState(objBehaviour newState)
    {

        ExitState();

        state_acBehaviour = newState;

        EnterState();
    }

    void EnterState()
    {
        // Perform actions when exiting a state
        switch (state_acBehaviour)
        {
            case objBehaviour.off:
                objEnterState_off();
                break;
            case objBehaviour.initialized:
                break;
            case objBehaviour.activated:
                objEnterState_activated();
                break;
            case objBehaviour.correct:
                break;
        }
    }

    private void objEnterState_activated()
    {
        if (!isThisTV)
        {
            acAnimator.SetTrigger("acOn");
            acScreenCover.SetActive(false);
            remoteScreenCover.SetActive(false);
            acLight.enabled = true;
            remoteLight.enabled = true;
        }
        else
        {
            remoteTVRenderer.material.SetTexture("_MainTex", textureTVRemote[1]);
            //script_channel.changeChannel(script_channel.channel);
            rendererAC.material.SetTexture("_MainTex", textureACList[1]);
            acLight.enabled = true;
            remoteLight.enabled = true;
        }
    }

    private void objEnterState_off()
    {
        if (!isThisTV)
        {
            acAnimator.SetTrigger("acOff");
            acScreenCover.SetActive(true);
            remoteScreenCover.SetActive(true);
            acLight.enabled = false;
            remoteLight.enabled = false;
        }
        else
        {
            remoteTVRenderer.material.SetTexture("_MainTex", textureTVRemote[0]);
            rendererAC.material.SetTexture("_MainTex", textureACList[5]);
            acLight.enabled = false;
            remoteLight.enabled = false;
        }
    }

    void ExitState()
    {
        // Perform actions when exiting a state
        switch (state_acBehaviour)
        {
            case objBehaviour.off:

                break;
            case objBehaviour.initialized:

                break;
            case objBehaviour.activated:
    
                break;
            case objBehaviour.correct:

                break;
        }
    }

    void PerformStateBehaviour()
    {
        // Perform actions when exiting a state
        switch (state_acBehaviour)
        {
            case objBehaviour.off:
                if (console_switch != null)
                {
                    console_switch.enabled = false;
                }
                break;
            case objBehaviour.initialized:

                break;
            case objBehaviour.activated:
                if (console_switch != null)
                {
                    if(isTVAV)
                    {
                        console_switch.enabled = true;
                    }
                }
                break;
            case objBehaviour.correct:

                break;
        }
    }

    public bool _objActivate
    {
        get { return objActive; }
        set
        {
            if (objActive != value)
            {
                objActive = value;
                StartCoroutine(DelayedActivation(0.33f, objActive));
                objToDeactivate.SetActive(!objActive);
            }
        }
    }
    IEnumerator DelayedActivation(float delay, bool activate)
    {
        yield return new WaitForSeconds(delay);
        objToActivate.SetActive(activate);
    }


    public IEnumerator acPopUpWarning()
    {
        float elapsedTime = 0f;
        ACTextWarningScript script_acText = TouchCodeScript.selectedObject.GetComponent<ACTextWarningScript>();
        while (elapsedTime < script_acText.timer)
        {
            script_acText.textToShow.text = script_acText.text;
            yield return null;
            elapsedTime += Time.deltaTime;
        }

        script_acText.textToShow.text = "";
    }

    //Refer this to select touchcode
    public void ACButtonPressed(bool isWantToTurnOn)
    {
        if (TouchCodeScript.selectedObject != null && TouchCodeScript.selectedObject.name == ac_remoteSwitch.name)
        {
            if(isWantToTurnOn)
            {
                ChangeState(objBehaviour.activated);
                ObjConditionScript.global_acStatsIndex[acIndex] = ObjConditionScript.acObjBehaviour.activated;
            } else
            {
                ChangeState(objBehaviour.off);
                ObjConditionScript.global_acStatsIndex[acIndex] = ObjConditionScript.acObjBehaviour.off;
                isACActive = false;
            }
            
        }
    }

    public void changeACValue(int val)
    {
        if (state_acBehaviour == objBehaviour.activated || state_acBehaviour == objBehaviour.correct)
        {
            if (defValue - 20 + val > -1 && defValue - 20 + val < 7)
            {
                defValue += (changeVal * val);
                try
                {
                    script_obj.objACStats();
                    rendererRemote.material.SetTexture("_MainTex", textureACList[defValue - 20]);
                    rendererAC.material.SetTexture("_EmissionMap", emissionACList[defValue - 20]);
                    rendererAC.material.SetTexture("_MainTex", textureACList[defValue - 20]);
                }
                catch
                {
                    Debug.Log("Ac Value is out of bounds");
                }
            }
            else
            {
                Debug.Log("Ac Value is out of bounds");
            }
        }
    }

    public void activateACCollider()
    {
        if (TouchCodeScript.selectedObject.GetComponentInParent<CheckACValueScript>() != null)
        {
            _objActivate = true;
            ChangeState(objBehaviour.initialized);
            if (!ObjConditionScript.global_acStatsIndex.ContainsKey(acIndex))
            {
                ObjConditionScript.global_acStatsIndex.Add(acIndex, ObjConditionScript.acObjBehaviour.initialized);
            }
        }
    }

    public void deactivateACCollider()
    {
            _objActivate = false;
        //ChangeState(acBehaviour.off);
    }

}
