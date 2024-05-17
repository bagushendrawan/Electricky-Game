using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVRemoteScript : MonoBehaviour
{
    public int channel;
    public CheckACValueScript script_value;
    public ObjConditionScript script_obj;

    public void changeChannel(int channel)
    {
        if(script_value.state_acBehaviour == CheckACValueScript.acBehaviour.activated || script_value.state_acBehaviour == CheckACValueScript.acBehaviour.correct || script_value.state_acBehaviour == CheckACValueScript.acBehaviour.initialized)
        {
            if (channel != 4)
            {
                script_value.defValue = channel;
                script_value.isTVAV = false;
                script_obj.objACStats();
                script_value.rendererAC.material.SetTexture("_MainTex", script_value.textureACList[channel]);
                ObjConditionScript.obj_dataList[script_value.consoleIndex].tronic_correct_Q = true;
            }
            else
            {
                script_value.isTVAV = true;
                script_value.defValue = channel;
                script_obj.objACStats();
                
                if (ObjConditionScript.obj_dataList[script_value.consoleIndex].tronic_correct_Q == false)
                {
                    ObjConditionScript.obj_dataList[script_value.consoleIndex].tronic_correct_Q = true;
                    ObjConditionScript.obj_dataList[script_value.consoleIndex].tronic_timerCoroutine = StartCoroutine(script_obj.timerDecreasePerSec(script_value.consoleIndex));
                    script_obj.script_waitTimer.StartTimer(ObjConditionScript.obj_dataList[script_value.consoleIndex].tronic_timer, script_value.consoleIndex, false);
                }

                script_value.rendererAC.material.SetTexture("_MainTex", null);
            }
        } else
        {
            Debug.LogError("The TV is not activated");
        }
        
    }

}
