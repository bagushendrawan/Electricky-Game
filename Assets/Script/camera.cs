using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    
   public void ActivateCamera(GameObject selectedObject)
    {
        // Deactivate all cameras in the scene
        Camera[] allCameras = Camera.allCameras;
        foreach (Camera camera in allCameras)
        {
            camera.gameObject.SetActive(false);
        }
        
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
}
