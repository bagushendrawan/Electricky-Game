using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TouchCodeScript : MonoBehaviour
{
    private FSMCameraRoomScript script_cameraState;
    private ObjConditionScript script_objCondition;
    public static GameObject selectedObject;
    private Collider objCollider;
    [SerializeField] private CheckACValueScript script_acObj;

    private Vector2 startTouchPos;
    private Vector2 endTouchPos;

    private ObjColliderToActivateScript script_colliderToActivate;
    private BedLampActivateScript script_objToActivate;
    [SerializeField] private CPUButtonScript script_cpuButton;
    private bool isSwipeLocked = false;
    //public bool secLevel = false;
    public float swipeLockTimer;

    void Start()
    {

        script_cameraState = GetComponent<FSMCameraRoomScript>();
        script_objCondition = GetComponent<ObjConditionScript>();
    }
    void Update()
    {
        touchHandler();
    }

    //Lock timer so it doesnt crash
    IEnumerator SwipeTimer(float time)
    {
        float timer = time;
        isSwipeLocked = true;
        while (timer >= 0 && isSwipeLocked)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                isSwipeLocked = false;
                yield break; // Exit the coroutine if the taskTimer has reached zero
            }
            yield return null; // Wait for the next frame
        }
    }

    public void activateSecCollider()
    {
        if (selectedObject.GetComponent<ObjColliderToActivateScript>() != null)
        {
            script_colliderToActivate = selectedObject.GetComponent<ObjColliderToActivateScript>();
            if (!script_cameraState.currentVirtualCamera.CompareTag("mainVirtualCamera"))
            {
                script_colliderToActivate.objCol.enabled = true;
                objCollider = selectedObject.GetComponent<Collider>();
                objCollider.enabled = false;
            }
        }
    }
    public void deactiveSecCollider()
    {
        if(script_cameraState.currentVirtualCamera.CompareTag("mainVirtualCamera"))
        {
            script_colliderToActivate.objCol.enabled = false;
            objCollider.enabled = true;
        }
       
    }

    public void select(string Tag)
    {

        switch (Tag)
        {
            case "firstLevel":
                print($"tag : {Tag}");
                script_cameraState.activateNewCamera(selectedObject);
                //script_cameraState.ChangeState(CameraStatesScript.cameraState.first);
                script_acObj.activateACCollider();
                activateSecCollider();

                if(selectedObject.GetComponent<BedLampActivateScript>() != null)
                {
                    script_objToActivate = selectedObject.GetComponent<BedLampActivateScript>();
                }
                break;
            case "secondLevel":
                print($"tag : {Tag}");
                script_cameraState.activateNewCamera(selectedObject);
                //script_cameraState.ChangeState(CameraStatesScript.cameraState.second);
                script_acObj.activateACCollider();
                activateSecCollider();

                if (selectedObject.GetComponent<BedLampActivateScript>() != null)
                {
                    script_objToActivate = selectedObject.GetComponent<BedLampActivateScript>();
                }
                break;
            case "plusButton":
                print($"tag : {Tag}");
                script_acObj.changeACValue(1);
                break;
            case "minButton":
                print($"tag : {Tag}");
                script_acObj.changeACValue(-1);
                break;
            case "Switch":
                print($"tag : {Tag}");
                script_objCondition.objSwitch(selectedObject);
                script_cpuButton.CPUButtonPressed();
                break;
            case "Electricity":
                print($"tag : {Tag}");
                script_objCondition.eleButton();
                break;
            case "changeScene":
                print($"tag : {Tag}");
                selectedObject.GetComponent<DoorSceneScript>().changeRoom();
                //SceneManager.LoadSceneAsync(selectedObject.GetComponent<DoorSceneScript>().sceneIndex);
                //script_objCondition.isElectricityAssetFound = false;
                //script_objCondition.deactivateObjSwitch();
                break;
            case "backScene":
                print($"tag : {Tag}");
                //SceneManager.LoadSceneAsync(1);
                ////script_objCondition.isElectricityAssetFound = false;
                ////script_objCondition.activateObjSwitch();
                //script_cameraState.changeRoomState(script_cameraState.currentCameraIndex);
                break;
            case "Selectable":
                print($"tag : {Tag}");
                if (selectedObject.GetComponent<ACTextWarningScript>() != null)
                {
                    StartCoroutine(script_acObj.acPopUpWarning());
                }
                break;
            default:
                print($"tag : {Tag}");
                break;
        }
    }

    private void touchHandler()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // Assuming you're interested in the first touch

            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;
                startTouchPos = touch.position;

                if (Physics.Raycast(ray, out hit))
                {
                    GameObject touchedObject = hit.transform.gameObject;

                    selectedObject = touchedObject;
                    print($"hit {touchedObject.tag}");

                    select(touchedObject.tag);
                }
            }

            if (touch.phase == TouchPhase.Moved && script_cameraState.currentVirtualCamera.CompareTag("firVirtualCamera"))
            {
                //Error here
                script_objToActivate.obj.transform.Rotate(0, -touch.deltaPosition.x * script_objToActivate.turnspeed * Time.deltaTime, 0);
            }

            if (touch.phase == TouchPhase.Ended)
            {
                endTouchPos = touch.position;
                if (script_cameraState.currentVirtualCamera.CompareTag("mainVirtualCamera"))
                {
                    if (startTouchPos.y < Screen.height / 2)
                    {
                        if (endTouchPos.x < startTouchPos.x)
                        {
                            Debug.Log("Next Swipe");
                            //if (!isSwipeLocked)
                            //{
                                script_cameraState.nextSwipe();
                                //StartCoroutine(SwipeTimer(swipeLockTimer));
                            //}
                            //else
                            //{
                            //    Debug.Log("SWIPE NEXT IS LOCKED TIMER");
                            //}

                        }

                        if (endTouchPos.x > startTouchPos.x)
                        {
                            Debug.Log("Prev Swipe");
                            //if (!isSwipeLocked)
                            //{
                                script_cameraState.prevSwipe();
                            //    StartCoroutine(SwipeTimer(swipeLockTimer));
                            //}
                            //else
                            //{
                            //    Debug.Log("SWIPE PREV IS LOCKED TIMER");
                            //}

                        }
                    }
                }

            }
        }
    }

}
