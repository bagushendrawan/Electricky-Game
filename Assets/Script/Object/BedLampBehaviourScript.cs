using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedLampBehaviourScript : MonoBehaviour
{
    public int bedroomLampIndex;
    public int bedlampIndex;

    //Set the bedlampcorrect to false first
    public void bedroomLampCheck()
    {
        if(ObjConditionScript.obj_dataList[bedroomLampIndex].tronic_active_Q == true)
        {
            ObjConditionScript.obj_dataList[bedlampIndex].tronic_correct_Q = false;
        } else
        {
            ObjConditionScript.obj_dataList[bedlampIndex].tronic_correct_Q = true;
        }
    }
}
