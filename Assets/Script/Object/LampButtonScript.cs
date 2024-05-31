using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampButtonScript : MonoBehaviour
{
    [SerializeField] private GameObject lamp_switch;
    //Refer this to select touchcode
    public void LampButtonPressed()
    {
        if (TouchCodeScript.selectedObject != null && TouchCodeScript.selectedObject.name == lamp_switch.name)
        {
            GameObject selected = TouchCodeScript.selectedObject;
            Animator anim = selected.GetComponent<Animator>();
            Debug.Log("LAMP HIT");
            anim.SetTrigger("lampOn");
        }
    }
}
