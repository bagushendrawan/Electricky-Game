using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSceneScript : MonoBehaviour
{
    public int roomIndexToGo;
    public FSMCameraRoomScript script_cameraState;
    public void changeRoom()
    {
        foreach (GameObject roomSides in script_cameraState.roomSides)
        {
            Debug.Log("CHANGE POS HERE");
            //Vector3 currentPos = roomSides.transform.position;
            //currentPos.y = 0f;
            //transform.position = currentPos;
            roomSides.transform.position = new Vector3(roomSides.transform.position.x, 0, roomSides.transform.position.z);
        }
        foreach (GameObject room in ObjConditionScript.obj_roomList)
        {
            room.SetActive(false);
        }
        ObjConditionScript.obj_roomList[roomIndexToGo].SetActive(true);


    }
}
