using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjParticleScript : MonoBehaviour
{
    public GameObject obj_particle;
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
            obj_particle.SetActive(true);
        }
        else
        {
            obj_particle.SetActive(false);
        }
    }
}
