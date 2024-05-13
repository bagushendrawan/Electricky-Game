using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjLightScript : MonoBehaviour
{
    public Light obj_light;
    [Header("Please refer to game manajer obj condition")]
    public int obj_index;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(ObjConditionScript.obj_dataList[obj_index].tronic_active_Q)
        {
            obj_light.enabled = true;
        } else
        {
            obj_light.enabled = false;
        }
    }
}
