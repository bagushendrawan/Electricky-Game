using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateACScript : MonoBehaviour
{
    public ScriptableObjectScript scriptable_script;
    public Animator animatorAC;
    public GameObject particleAC;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!scriptable_script.global_eleOn_Q)
        {
            animatorAC.SetTrigger("acOff");
            particleAC.SetActive(false);
        }
    }
}
