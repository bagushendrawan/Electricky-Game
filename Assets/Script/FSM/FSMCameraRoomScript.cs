using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FSMCameraRoomScript : MonoBehaviour
{
    //save current active camera
    public CinemachineVirtualCamera currentVirtualCamera;

    //control camera level act like a stack, could be optimized with stack
    public Stack<CinemachineVirtualCamera> virCameraList = new();

    [HideInInspector] public int currentCameraIndex;
    [HideInInspector] public int currentState;

    [Header("Assign The Camera and Wall Sides")]
    public List<CinemachineVirtualCamera> roomCamera;
    public List<GameObject> roomSides;

    [Header("Wall Animation")]
    //[SerializeField] private float distance;
    //[SerializeField] private float speed;
    private float wallMoveTimer = 1.1f;
    private float wallMoveSpeed = 1.75f;
    [SerializeField] private string animTriggerUp;
    [SerializeField] private string animTriggerDown;

    public enum roomCameraState
    {
        front,
        right,
        back,
        left
    }

    private roomCameraState state_roomCamera;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Game Start");
        deactivateAllCameras();
        virCameraList.Push(roomCamera[0]);
        currentVirtualCamera = virCameraList.Peek();
        currentCameraIndex = 0;
        currentVirtualCamera.enabled = true;
    }


    // Update is called once per frame
    void Update()
    {
        PerformStateActions();
    }

    public void ChangeState(roomCameraState newState)
    {

        ExitState();

        state_roomCamera = newState;

        EnterState();
    }

    void EnterState()
    {
        // Perform actions when entering a state
        switch (state_roomCamera)
        {
            case roomCameraState.front:
                moveObject_front();
                break;
            case roomCameraState.left:
                moveObject_left();
                break;
            case roomCameraState.back:
                moveObject_back();
                break;
            case roomCameraState.right:
                moveObject_right();
                break;
        }
    }

    private void moveObject_right()
    {
        StartCoroutine(MoveToY(roomSides[2], wallMoveTimer, 0));
        StartCoroutine(MoveToY(roomSides[3], wallMoveTimer, 0));
        StartCoroutine(MoveToY(roomSides[0], wallMoveTimer, 10));
        StartCoroutine(MoveToY(roomSides[1], wallMoveTimer, 10));
    }

    private void moveObject_back()
    {
        StartCoroutine(MoveToY(roomSides[3], wallMoveTimer, 0));
        StartCoroutine(MoveToY(roomSides[0], wallMoveTimer, 0));
        StartCoroutine(MoveToY(roomSides[2], wallMoveTimer, 10));
        StartCoroutine(MoveToY(roomSides[1], wallMoveTimer, 10));
    }

    private void moveObject_left()
    {
        StartCoroutine(MoveToY(roomSides[0], wallMoveTimer, 0));
        StartCoroutine(MoveToY(roomSides[1], wallMoveTimer, 0));
        StartCoroutine(MoveToY(roomSides[2], wallMoveTimer, 10));
        StartCoroutine(MoveToY(roomSides[3], wallMoveTimer, 10));
    }

    private void moveObject_front()
    {
        StartCoroutine(MoveToY(roomSides[2], wallMoveTimer, 0));
        StartCoroutine(MoveToY(roomSides[1], wallMoveTimer, 0));
        StartCoroutine(MoveToY(roomSides[0], wallMoveTimer, 10));
        StartCoroutine(MoveToY(roomSides[3], wallMoveTimer, 10));
    }

    void ExitState()
    {
        // Perform actions when exiting a state
        switch (state_roomCamera)
        {
            case roomCameraState.front:

                break;
            case roomCameraState.left:

                break;
            case roomCameraState.back:

                break;
            case roomCameraState.right:

                break;
        }
    }
    
    void PerformStateActions()
    {
        // Perform actions based on the current state
        switch (state_roomCamera)
        {
            case roomCameraState.front:

                break;
            case roomCameraState.left:

                break;
            case roomCameraState.back:

                break;
            case roomCameraState.right:

                break;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void nextSwipe()
    {
        deactivateAllCameras();
        if (currentVirtualCamera != roomCamera[3])
        {
            currentCameraIndex++;
            currentVirtualCamera.enabled = false;
            virCameraList.Pop();
            roomCamera[currentCameraIndex].enabled = true;
            virCameraList.Push(roomCamera[currentCameraIndex]);
            currentVirtualCamera = virCameraList.Peek();
        }
        else
        {
            currentVirtualCamera.enabled = false;
            virCameraList.Pop();
            roomCamera[0].enabled = true;
            virCameraList.Push(roomCamera[0]);
            currentVirtualCamera = virCameraList.Peek();
            currentCameraIndex = 0;
        }
        changeRoomState(currentCameraIndex);
        //activateMove(currentCameraIndex);
    }

    public void prevSwipe()
    {
        deactivateAllCameras();

        if (currentVirtualCamera != roomCamera[0])
        {
            currentCameraIndex--;
            currentVirtualCamera.enabled = false;
            virCameraList.Pop();
            roomCamera[currentCameraIndex].enabled = true;
            virCameraList.Push(roomCamera[currentCameraIndex]);
            currentVirtualCamera = virCameraList.Peek();
        }
        else
        {
            currentVirtualCamera.enabled = false;
            virCameraList.Pop();
            roomCamera[3].enabled = true;
            virCameraList.Push(roomCamera[3]);
            currentVirtualCamera = virCameraList.Peek();
            currentCameraIndex = 3;
        }
        changeRoomState(currentCameraIndex);
        //activateMove(currentCameraIndex);
    }

    public void changeRoomState(int index)
    {
        switch (index)
        {
            case 0:
                ChangeState(roomCameraState.front);
                break;
            case 1:
                ChangeState(roomCameraState.right);
                break;
            case 2:
                ChangeState(roomCameraState.back);
                break;
            case 3:
                ChangeState(roomCameraState.left);
                break;
        }
    }

    IEnumerator MoveToY(GameObject obj, float duration, float targetY)
    {
        float startY = obj.transform.position.y;
        float timer = 0f;

        while (timer < duration)
        { 
            timer += Time.deltaTime * wallMoveSpeed;
            float newY = Mathf.Lerp(startY, targetY, timer);
            obj.transform.position = new Vector3(obj.transform.position.x, newY, obj.transform.position.z);
            yield return null;
        }
    }

    void moveObjDown(GameObject obj, float duration, float pos)
    {
        Animator anim = obj.GetComponent<Animator>();
        
        if (obj.transform.position.y > 0)
        {   
            //Debug.Log(obj.name + animTriggerDown);
            anim.SetTrigger(animTriggerDown);
            StartCoroutine(DelayedExecution(obj, true, duration, pos));
        }

    }

    void moveObjUp(GameObject obj, float duration, float pos)
    {
        Animator anim = obj.GetComponent<Animator>();
        if (obj.transform.position.y <= 0)
        {
            //This one doesnt execute after back to rooms because the pos is 10
            anim.SetTrigger(animTriggerUp);
            StartCoroutine(DelayedExecution(obj, false, duration, pos));
        }


    }

    //fixed wall last position after anim
    private IEnumerator DelayedExecution(GameObject obj, bool con, float dur, float pos)
    {
        yield return new WaitForSeconds(dur);
        obj.transform.position = new Vector3(obj.transform.position.x, pos, obj.transform.position.z);
        //obj.SetActive(con);
    }

   public void deactivateAllCameras()
    {
        for (int i = 0; i < roomCamera.Count; i++)
        {
            if (currentVirtualCamera != roomCamera[i])
            {
                roomCamera[i].enabled = false;
            }
            else
            {
                currentCameraIndex = i;
                //Debug.Log("Current Cam Index :" + currentCameraIndex);
            }
        }
    }

    public void activateMove(int tag)
    {
        //Debug.Log("Move");
        switch (tag)
        {
            case 0:
                //Debug.Log("Move" + tag);
                moveObjUp(roomSides[0], 1.1f, 10);
                moveObjUp(roomSides[3], 1.1f, 10);
                moveObjDown(roomSides[2], 0, 0);
                moveObjDown(roomSides[1], 0, 0);
                break;
            case 1:
                //Debug.Log("Move" + tag);
                moveObjUp(roomSides[0], 1.1f, 10);
                moveObjUp(roomSides[1], 1.1f, 10);
                moveObjDown(roomSides[2], 0, 0);
                moveObjDown(roomSides[3], 0, 0);
                break;
            case 2:
                //Debug.Log("Move" + tag);
                moveObjUp(roomSides[2], 1.1f, 10);
                moveObjUp(roomSides[1], 1.1f, 10);
                moveObjDown(roomSides[3], 0, 0);
                moveObjDown(roomSides[0], 0, 0);
                break;
            case 3:
                //Debug.Log("Move" + tag);
                moveObjUp(roomSides[2], 1.1f, 10);
                moveObjUp(roomSides[3], 1.1f, 10);
                moveObjDown(roomSides[0], 0, 0);
                moveObjDown(roomSides[1], 0, 0);
                break;
        }
    }

    //activate obj camera, get the selected objectcamerascript.cam in touch code
    public void activateNewCamera(GameObject selectedObject)
    {
        if (selectedObject.GetComponent<ObjCameraScript>().cam != virCameraList.Peek())
        {
            currentVirtualCamera.enabled = false;
            virCameraList.Push(selectedObject.GetComponent<ObjCameraScript>().cam);
            currentVirtualCamera = virCameraList.Peek();
            currentVirtualCamera.enabled = true;
            activateSwitch(selectedObject, true);
        }
    }

    /// <summary>
    /// Back button pressed
    /// </summary>
    public void activatePrevCamera()
    {
        currentVirtualCamera.enabled = false;
        virCameraList.Pop();
        currentVirtualCamera = virCameraList.Peek();
        currentVirtualCamera.enabled = true;
    }

    //activate obj collider after switch cam
    void activateSwitch(GameObject selectedObject, bool col)
    {
        Collider[] objectToActivate = selectedObject.GetComponentsInChildren<Collider>(true);
        foreach (Collider x in objectToActivate)
        {
            if (x.gameObject.name == "Switch")
            {
                x.enabled = col;
                //Debug.Log("Object Activated");
            }
        }
    }


}
