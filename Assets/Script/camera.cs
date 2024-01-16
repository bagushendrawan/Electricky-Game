using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    [HideInInspector] public GameObject prevCamera;
    public void ActivateCamera(GameObject selectedObject)
    {
        prevCamera = assignPrevCamera(Camera.main.name);
        deactivateAllCamera();

        Camera selectedCamera = selectedObject.GetComponentInChildren<Camera>(true);
        if (selectedCamera != null)
        {
            // Activate the specified camera
            selectedCamera.gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("Camera not found");
        }
    }

    public void deactivateAllCamera()
    {
        // Deactivate all cameras in the scene
        Camera[] allCameras = Camera.allCameras;
        foreach (Camera camera in allCameras)
        {
            camera.gameObject.SetActive(false);
        }
    }

    GameObject assignPrevCamera(string cameraNames)
    {
        GameObject cameraToActivate = GameObject.Find(cameraNames);
        return cameraToActivate;
    }
}
