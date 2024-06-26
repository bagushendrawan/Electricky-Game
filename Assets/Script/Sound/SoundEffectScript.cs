using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectScript : MonoBehaviour
{
    public AudioSource soundEffect;
    public AudioClip soundClip;

    public void playSound()
    {
        soundEffect.PlayOneShot(soundClip);
    }
}
