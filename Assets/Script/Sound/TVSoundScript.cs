using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVSoundScript : MonoBehaviour
{
    public int tv_index;
    public int game_index;
    public List<AudioSource> audioSource;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (ObjConditionScript.obj_dataList[tv_index].tronic_active_Q)
        {

            if(ObjConditionScript.obj_dataList[game_index].tronic_correct_Q && ObjConditionScript.obj_dataList[tv_index].tronic_active_Q && ObjConditionScript.obj_dataList[game_index].tronic_active_Q)
            {
                audioSource[0].enabled = false;
                audioSource[1].enabled = true;
            } else
            {
                audioSource[0].enabled = true;
                audioSource[1].enabled = false;
            }
        }
        else
        {
            audioSource[0].enabled = false;
        }
    }
}
