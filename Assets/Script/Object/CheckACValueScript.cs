using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CheckACValueScript : MonoBehaviour
{
    private bool objActive = false;
    private bool isAnimRun = false;
    public int defValue;
    public int changeVal;
    public int valueCondition;
    public GameObject objToActivate;
    public GameObject objToDeactivate;
    public Collider ACButton;
    public List<Texture2D> textureACList = new();
    public Renderer rendererRemote;
    public Renderer rendererAC;
    [SerializeField] TMP_Text valText;
    [SerializeField] private GameObject ac_remoteSwitch;
    [SerializeField] private Animator acAnimator;

    // Update is called once per frame
    void Update()
    {
        updateValue();
        valueCheck(valueCondition);
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

    void valueCheck(int con)
    {
        if (defValue == con)
        {
            ACButton.enabled = true;
        }
        else
        {
            ACButton.enabled = false;
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
            Debug.Log("AC Anim Hit!");
            if(isWantToTurnOn)
            {
                Debug.Log("AC Anim On!");
                acAnimator.SetTrigger("acOn");
            } else
            {
                Debug.Log("AC Anim Off!");
                acAnimator.SetTrigger("acOff");
            }
            
        }
    }

    public void changeACValue(int val)
    {
        if (defValue - 21 + val > -1 && defValue - 20 + val < 6)
        {
            defValue += (changeVal * val);
            try 
            { 
                rendererRemote.material.SetTexture("_MainTex", textureACList[defValue - 21]);
                rendererAC.material.SetTexture("_MainTex", textureACList[defValue - 21]);
            } catch 
            {
                Debug.Log("Ac Value is out of bounds");
            }
        } else
        {
            Debug.Log("Ac Value is out of bounds");
        }
       
    }

    public void activateACCollider()
    {
        if (TouchCodeScript.selectedObject.GetComponentInParent<CheckACValueScript>() != null)
        {
            _objActivate = true;
            Debug.Log("objActivated");
        }
    }

    public void deactivateACCollider()
    {
            _objActivate = false;
            Debug.Log("objDeactivated");
    }
}
