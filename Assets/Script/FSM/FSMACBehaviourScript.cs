using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FSMACBehaviourScript : MonoBehaviour
{
    private bool objActive = false;
    public int defValue;
    public int changeVal;
    public int valueCondition;
    public GameObject objToActivate;
    public GameObject objToDeactivate;
    [SerializeField] private GameObject ac_remoteSwitch;
    [SerializeField] private Animator acAnimator;
    [SerializeField] TMP_Text valText;

    public List<Texture2D> textureACList = new();
    public List<Texture2D> emissionACList = new();

    public Renderer rendererRemote;
    public Renderer rendererAC;

    [SerializeField] private ObjConditionScript script_obj;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        PerformStateBehaviour();
    }

    public enum acBehaviour
    {
        initialized,
        activated,
        correct,
        deactivated
    }

    public acBehaviour state_acBehaviour;

    public void ChangeState(acBehaviour newState)
    {
        // Exit the current state
        ExitState();
        // Set the new state
        state_acBehaviour = newState;
        // Enter the new state
        EnterState();
    }

    void EnterState()
    {
        // Perform actions when exiting a state
        switch (state_acBehaviour)
        {
            case acBehaviour.initialized:

                break;
            case acBehaviour.activated:

                break;
            case acBehaviour.correct:

                break;
            case acBehaviour.deactivated:

                break;
        }
    }

    void ExitState()
    {
        // Perform actions when exiting a state
        switch (state_acBehaviour)
        {
            case acBehaviour.initialized:

                break;
            case acBehaviour.activated:

                break;
            case acBehaviour.correct:

                break;
            case acBehaviour.deactivated:

                break;
        }
    }

        void PerformStateBehaviour()
        {
            // Perform actions when exiting a state
            switch (state_acBehaviour)
            {
                case acBehaviour.initialized:

                    break;
                case acBehaviour.activated:

                    break;
                case acBehaviour.correct:

                    break;
                case acBehaviour.deactivated:

                    break;
            }
        }

    public IEnumerator acPopUpWarning()
    {
        Debug.Log("Text Pop Up!");
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
            if (isWantToTurnOn)
            {
                ChangeState(acBehaviour.activated);
                Debug.Log("AC Anim On!");
                acAnimator.SetTrigger("acOn");
                
            }
            else
            {
                ChangeState(acBehaviour.initialized);
                Debug.Log("AC Anim Off!");
                acAnimator.SetTrigger("acOff");
            }

        }
    }

    public void changeACValue(int val)
    {
        if (state_acBehaviour == acBehaviour.activated && defValue - 20 + val > -1 && defValue - 20 + val < 7)
        {
            defValue += (changeVal * val);
            Debug.Log("Value " + defValue);
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

    public void activateACCollider()
    {
        if (TouchCodeScript.selectedObject.GetComponentInParent<CheckACValueScript>() != null)
        {
            _objActivate = true;
            ChangeState(acBehaviour.initialized);
            Debug.Log("objActivated");
        }
    }

    public void deactivateACCollider()
    {
        _objActivate = false;
        ExitState();
        Debug.Log("objDeactivated");
    }

    public bool _objActivate
    {
        get { return objActive; }
        set
        {
            if (objActive != value)
            {
                objActive = value;
                objToActivate.SetActive(objActive);
                objToDeactivate.SetActive(!objActive);
            }
        }
    }

    void updateValue()
    {
        string s;
        s = defValue.ToString();
        valText.text = s;
    }

}
