using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundObjectScript : MonoBehaviour
{
    public int obj_index;
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        if(ObjConditionScript.obj_dataList[obj_index].tronic_active_Q)
        {
            audioSource.enabled = true;
        } else
        {
            audioSource.enabled = false;
        }
    }
}
