using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElecOutScript : MonoBehaviour
{
    public ScriptableObjectScript scriptable_script;
    public AudioSource audioSource;
    public AudioClip audioClip;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!scriptable_script.global_eleOn_Q)
        {
            audioSource.PlayOneShot(audioClip);
        }
    }
}
