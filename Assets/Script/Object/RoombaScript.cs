using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoombaScript : MonoBehaviour
{
    public Animator roomba_animator;
    public int roombaDataIndex;
    public Renderer roombaRenderer;
    public List<Texture2D> roombaTexture;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(ObjConditionScript.obj_dataList[roombaDataIndex].tronic_active_Q)
        {
            roomba_animator.SetBool("roomba",true);
            roombaRenderer.material.SetTexture("_MainTex", roombaTexture[1]);
        } else
        {
            roomba_animator.SetBool("roomba", false);
            roombaRenderer.material.SetTexture("_MainTex", roombaTexture[0]);
        }
    }
}
