using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPUEffectScript : MonoBehaviour
{
    public Light cpuLight;
    public GameObject monitorScreen;
    public int cpuIndex;

    private void Update()
    {
        if(ObjConditionScript.obj_dataList[cpuIndex].tronic_active_Q)
        {
            cpuLight.enabled = true;
            monitorScreen.SetActive(true);
        } else
        {
            cpuLight.enabled = false;
            monitorScreen.SetActive(false);
        }
    }
}
