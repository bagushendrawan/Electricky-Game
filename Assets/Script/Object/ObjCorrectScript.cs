using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjCorrectScript : MonoBehaviour
{
    public int correctIndex;
    
    public void Correct()
    {
        ObjConditionScript.obj_dataList[correctIndex].tronic_correct_Q = !ObjConditionScript.obj_dataList[correctIndex].tronic_correct_Q;
    }
}
