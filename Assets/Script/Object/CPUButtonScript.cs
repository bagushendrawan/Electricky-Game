using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPUButtonScript : MonoBehaviour
{
    [SerializeField] private GameObject cpu_switch;
    //Refer this to select touchcode
    public void CPUButtonPressed()
    {
        if (TouchCodeScript.selectedObject != null && TouchCodeScript.selectedObject.name == cpu_switch.name)
        {
            GameObject selected = TouchCodeScript.selectedObject;
            Animator anim = selected.GetComponentInParent<Animator>();
            Debug.Log("CPU Hit!");
            anim.SetTrigger("cpuOn");
        }
    }
}
