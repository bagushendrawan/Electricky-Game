using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class singletonStats : MonoBehaviour
{
    private static singletonStats _instance;
    public int timer { get; private set; }
    public int eleCapacity { get; private set; }

    public static singletonStats Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<singletonStats>();

                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject("GameManager");
                    _instance = singletonObject.AddComponent<singletonStats>();
                }
            }

            return _instance;
        }
    }

    // Other GameManager functionality...

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
