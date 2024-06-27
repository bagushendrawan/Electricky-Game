using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjAnimationScript : MonoBehaviour
{
    public Animator obj_animator;
    public GameObject obj_light;
    public int objDataIndex;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (ObjConditionScript.obj_dataList[objDataIndex].tronic_active_Q)
        {
            obj_light.SetActive(true);
            obj_animator.SetBool("on",true);
        }
        else
        {
            obj_light.SetActive(false);
            obj_animator.SetBool("on",false);
        }
    }
}
