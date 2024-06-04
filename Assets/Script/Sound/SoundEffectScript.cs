using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectScript : MonoBehaviour
{
    private AudioSource soundEffect;
    public AudioClip soundClip;

    public void playSound()
    {
        soundEffect = GetComponent<AudioSource>();
        soundEffect.PlayOneShot(soundClip);
    }
}
