using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endCondition : MonoBehaviour
{
    public bool winCond;
    public scriptableObject scriptableScript;
    public GameObject canvasWin;

    void Awake()
    {
        for (int i = 0; i < scriptableScript.dataList.Count; i++)
        {
            scriptableScript.dataList[i].taskStats = false;
            scriptableScript.dataList[i].taskActive = false;
        }
    }
    void Update()
    {
        winCond = winCheck();
        if (winCond == true)
        {
            if (canvasWin != null)
            {
                Time.timeScale = 0;
                canvasWin.SetActive(true);
                winCond = false;
            }
        }
    }

    bool winCheck()
    {
        for (int i = 0; i < scriptableScript.dataList.Count; i++)
        {
            if (scriptableScript.dataList[i].taskStats == false)
            {
                return false;
            }
        }
        return true;
    }
}
