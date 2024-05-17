using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOnEnableScript : MonoBehaviour
{
    public FSMCameraRoomScript script_cameraState;
    public int cameraRoomState;

    private void OnEnable()
    {
        script_cameraState.roomSides.Clear();
        foreach(GameObject wall in GameObject.FindGameObjectsWithTag("wall"))
        {
            script_cameraState.roomSides.Add(wall);
        }

        if(script_cameraState.roomSides != null)
        {
            script_cameraState.roomSides.Sort(CompareGameObjectNames);
        }

        Debug.Log("ONENABLE CAMERA");
        cameraChanges();
    }

    //Sort by names
    private int CompareGameObjectNames(GameObject a, GameObject b)
    {
        return a.name.CompareTo(b.name);
    }

    private void cameraChanges()
    {
        script_cameraState.deactivateAllCameras();
        script_cameraState.roomCamera[cameraRoomState].enabled = true;
        script_cameraState.changeRoomState(cameraRoomState);
    }
}
