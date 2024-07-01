using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsoleMiddleScript : MonoBehaviour
{
    public CheckACValueScript script_ac;
    public ObjConditionScript script_obj;
    public int consoleIndex;
    public void objConsoleStats()
    {
        if (script_ac.isTVAV && ObjConditionScript.obj_dataList[script_ac.tvIndex].tronic_active_Q && ObjConditionScript.obj_dataList[script_ac.consoleIndex].tronic_active_Q)
        {
            ObjConditionScript.obj_dataList[script_ac.consoleIndex].tronic_correct_Q = true;
            script_ac.rendererAC.material.SetTexture("_MainTex", script_ac.textureACList[4]);
        } else
        {
            //script_ac.rendererAC.material.SetTexture("_MainTex", null);
            ObjConditionScript.obj_dataList[script_ac.consoleIndex].tronic_correct_Q = false;
        }
    }


    public void consoleOn()
    {
        ObjConditionScript.obj_dataList[consoleIndex].tronic_active_Q = !ObjConditionScript.obj_dataList[consoleIndex].tronic_active_Q;
        StartCoroutine(script_obj.eleDecreasePerSec(consoleIndex, ObjConditionScript.obj_dataList[consoleIndex].tronic_wattPerSec));
    }

    private void Update()
    {
        objConsoleStats();
    }
}
