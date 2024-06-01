using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVRemoteScript : MonoBehaviour
{
    public int channel;
    public CheckACValueScript script_value;
    public ObjConditionScript script_obj;
    [SerializeField] private ScriptableObjectScript script_scriptable;
    [SerializeField] private GameObject remote_switch;
    public void changeChannel(int channel)
    {
        if(script_scriptable.global_eleOn_Q)
        {
            if (script_value.state_acBehaviour == CheckACValueScript.objBehaviour.activated || script_value.state_acBehaviour == CheckACValueScript.objBehaviour.correct /*|| script_value.state_acBehaviour == CheckACValueScript.acBehaviour.initialized*/)
            {
                if (channel < 4)
                {
                    regularTVChannel(channel);
                }
                else if (channel == 5)
                {
                    nextTVChannel();
                }
                else if (channel == 6)
                {
                    prevTVChannel();
                }
                else
                {
                    avTVChannel(channel);
                }
            }
            else
            {
                Debug.LogError("The TV is not activated");
                script_value.rendererAC.material.SetTexture("_MainTex", null);
            }
        } else
        {
            script_value.rendererAC.material.SetTexture("_MainTex", null);
        }
    }

    private void avTVChannel(int channel)
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

    private void prevTVChannel()
    {
        script_value.defValue -= 1;
        if (script_value.defValue < 0) script_value.defValue = 3;
        script_value.isTVAV = false;
        script_obj.objACStats();
        script_value.rendererAC.material.SetTexture("_MainTex", script_value.textureACList[script_value.defValue]);
        ObjConditionScript.obj_dataList[script_value.consoleIndex].tronic_correct_Q = true;
    }

    private void nextTVChannel()
    {
        script_value.defValue += 1;
        if (script_value.defValue > 3) script_value.defValue = 0;
        script_value.isTVAV = false;
        script_obj.objACStats();
        script_value.rendererAC.material.SetTexture("_MainTex", script_value.textureACList[script_value.defValue]);
        ObjConditionScript.obj_dataList[script_value.consoleIndex].tronic_correct_Q = true;
    }

    private void regularTVChannel(int channel)
    {
        Debug.Log("TV CHANNEL CHANGED");
        script_value.defValue = channel;
        script_value.isTVAV = false;
        script_obj.objACStats();
        script_value.rendererAC.material.SetTexture("_MainTex", script_value.textureACList[channel]);
        ObjConditionScript.obj_dataList[script_value.consoleIndex].tronic_correct_Q = true;
    }

    public void animActivate(int channel)
    {
        if (TouchCodeScript.selectedObject != null && TouchCodeScript.selectedObject.name == remote_switch.name)
        {
            GameObject selected = TouchCodeScript.selectedObject;
            Animator anim = selected.GetComponentInParent<Animator>();
            Debug.Log("TV REMOTE HIT");
            switch (channel)
            {
                case 0:
                    anim.SetTrigger("1");
                    break;
                case 1:
                    anim.SetTrigger("2");
                    break;
                case 2:
                    anim.SetTrigger("3");
                    break;
                case 3:
                    anim.SetTrigger("4");
                    break;
                case 4:
                    anim.SetTrigger("av");
                    break;
                case 5:
                    anim.SetTrigger("up");
                    break;
                case 6:
                    anim.SetTrigger("down");
                    break;
            }

        }
    }
}
