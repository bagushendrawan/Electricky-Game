using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class objValue : MonoBehaviour
{
    private bool objActive = false;
    public int defValue;
    public int changeVal;
    public GameObject objToActivate;
    public GameObject objToDeactivate;
    public GameObject AC;
    [SerializeField] TMP_Text valText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        updateValue();
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
        if (defValue >= con)
        {
            AC.SetActive(false);
        }
        else
        {
            AC.SetActive(true);
        }
    }
}
