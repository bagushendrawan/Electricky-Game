using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricBoxScript : MonoBehaviour
{
    public Animator elec_animator;
    public GameObject lightOn;
    public GameObject lightOff;
    public ScriptableObjectScript scriptable_script;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (scriptable_script.global_eleOn_Q)
        {
            lightOff.SetActive(false);
            lightOn.SetActive(true);
        }
        else
        {
            lightOn.SetActive(false);
            lightOff.SetActive(true);
            elec_animator.SetBool("on", false);
        }
    }
}
