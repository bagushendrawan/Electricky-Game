using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsoleBehaviourScript : MonoBehaviour
{
    [SerializeField] private GameObject console_switch;
    [SerializeField] private int consoleIndex;
    public Renderer consoleRenderer;
    public List<Texture2D> consoleTexture;
    //Refer this to select touchcode

    private void Update()
    {
        if (ObjConditionScript.obj_dataList[consoleIndex].tronic_active_Q)
        {
            Debug.Log("CONSOLE ON");
            consoleRenderer.material.SetTexture("_MainTex", consoleTexture[1]);
        } else
        {
            consoleRenderer.material.SetTexture("_MainTex", consoleTexture[0]);
        }
    }
    public void ConsoleButtonPressed()
    {
        if (TouchCodeScript.selectedObject != null && TouchCodeScript.selectedObject.name == console_switch.name)
        {
            
            GameObject selected = TouchCodeScript.selectedObject;
            Animator anim = selected.GetComponentInParent<Animator>();
            anim.SetTrigger("consoleOn");
        }
    }
}
