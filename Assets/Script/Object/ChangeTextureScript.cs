using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTextureScript : MonoBehaviour
{
    public Renderer textureRenderer;
    public List<Texture2D> textureList;
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
            textureRenderer.material.SetTexture("_MainTex", textureList[1]);
        } else
        {
            textureRenderer.material.SetTexture("_MainTex", textureList[0]);
        }
    }
}
