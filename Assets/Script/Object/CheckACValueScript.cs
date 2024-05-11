using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CheckACValueScript : MonoBehaviour
{
    private bool objActive = false;
    public int defValue;
    public int changeVal;
    public int valueCondition;
    public GameObject objToActivate;
    public GameObject objToDeactivate;
    public Collider ACButton;
    [SerializeField] TMP_Text valText;

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
            Debug.Log("AC Temp right");
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

    public void changeACValue(int val)
    {
        defValue += (changeVal * val);
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
