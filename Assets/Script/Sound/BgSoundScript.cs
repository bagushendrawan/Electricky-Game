using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgSoundScript : MonoBehaviour
{
    private AudioSource soundEffect;

    // Start is called before the first frame update
    void Start()
    {
        soundEffect = GetComponent<AudioSource>();
        soundEffect.Play();
    }

    private void OnDisable()
    {
        soundEffect.Stop();
    }

}
