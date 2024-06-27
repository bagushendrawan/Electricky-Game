using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BGMScript : MonoBehaviour
{
    //[SerializeField] AudioClip mainMenuClip;
    //[SerializeField] AudioClip levelOneClip;

    [HideInInspector] public AudioClip currentClip;
    [HideInInspector] public float volumeBGM;

    [SerializeField] AudioSource audioSource;

    public Slider mySlider;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        volumeSet();
    }

    //void SetBGM()
    //{

    //    int currentScene = SceneManager.GetActiveScene().buildIndex;
    //    switch (currentScene)
    //    {
    //        case (0):
    //            isBGMPlaying = false;
    //            currentClip = mainMenuClip;
    //            if (!isBGMPlaying)
    //            {
    //                audioSource.clip = mainMenuClip;
    //                audioSource.Play();
    //                Debug.Log("main menu audio");
    //            }
    //            break;

    //        case (1):
    //            isBGMPlaying = false;
    //            currentClip = mainMenuClip;
    //            if (!isBGMPlaying)
    //            {
    //                audioSource.clip = mainMenuClip;
    //                audioSource.Play();
    //                Debug.Log("main menu audio");
    //            }
    //            break;

    //        case (2):
    //            isBGMPlaying = false;
    //            currentClip = levelOneClip;
    //            if (!isBGMPlaying)
    //            {
    //                audioSource.clip = levelOneClip;
    //                audioSource.Play();
    //                Debug.Log("level one audio");
    //            }
    //            break;

    //        case (3):
    //            isBGMPlaying = false;
    //            currentClip = mainMenuClip;
    //            if (!isBGMPlaying)
    //            {
    //                audioSource.clip = levelOneClip;
    //                audioSource.Play();
    //                Debug.Log("level one audio");
    //            }
    //            break;

    //        case (4):
    //            isBGMPlaying = false;
    //            currentClip = mainMenuClip;
    //            if (!isBGMPlaying)
    //            {
    //                audioSource.clip = levelOneClip;
    //                audioSource.Play();
    //                Debug.Log("level one audio");
    //            }
    //            break;
    //    }
    //}

    public void volumeSet()
    {
        volumeBGM = mySlider.value;
        audioSource.volume = volumeBGM;
        //Debug.Log("Current Volume: " + mySlider.value);
    }

    public void volumeSet2(float value)
    {
        audioSource.volume = value;

    }

}
