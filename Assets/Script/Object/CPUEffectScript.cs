using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPUEffectScript : MonoBehaviour
{
    public Light cpuLight;
    public GameObject monitorScreen;
    public int cpuIndex;
    public List<Texture2D> textureCPUList = new();
    public Renderer rendererCPU;

    private void Update()
    {
        if(ObjConditionScript.obj_dataList[cpuIndex].tronic_active_Q)
        {
            rendererCPU.material.SetTexture("_MainTex", textureCPUList[1]);
            cpuLight.enabled = true;
            monitorScreen.SetActive(true);
        } else
        {
            rendererCPU.material.SetTexture("_MainTex", textureCPUList[0]);
            cpuLight.enabled = false;
            monitorScreen.SetActive(false);
        }
    }
}
