using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkLock : MonoBehaviour
{
    private rotateLock script_lock;

    private void Start()
    {
        script_lock = GetComponent<rotateLock>();
    }

    public void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.name == "Sphere")
        {
            Debug.Log("Collides with :" + "Sphere");
        }
    }
}
