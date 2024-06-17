using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class TouchCodeScript : MonoBehaviour
{
    private FSMCameraRoomScript script_cameraState;
    private ObjConditionScript script_objCondition;
    public static GameObject selectedObject;

    private Vector2 startTouchPos;
    private Vector2 endTouchPos;

    private BedLampActivateScript script_objToActivate;
    private ConsoleBehaviourScript script_console;
    [SerializeField] private CPUButtonScript script_cpuButton;
    private LampButtonScript script_lampButton;
    private bool isSwipeLocked = false;
    private Stack<Collider> stack_selectedCollider = new ();
    private Stack<Collider> stack_objCollider = new();
    //public bool secLevel = false;
    public float swipeLockTimer;

    void Start()
    {

        script_cameraState = GetComponent<FSMCameraRoomScript>();
        script_objCondition = GetComponent<ObjConditionScript>();
        script_lampButton = FindObjectOfType<LampButtonScript>();
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
            stack_objCollider.Push(selectedObject.GetComponent<ObjColliderToActivateScript>().objCol);
            stack_selectedCollider.Push(selectedObject.GetComponent<Collider>());
            stack_objCollider.Peek().enabled = true;
            stack_selectedCollider.Peek().enabled = false;
        }
    }
    public void deactiveSecCollider()
    {
        stack_objCollider.Pop().enabled = false;
        stack_selectedCollider.Pop().enabled = true;
    }

    public void select(string Tag)
    {

        switch (Tag)
        {
            case "firstLevel":
                print($"tag : {Tag}");
                script_cameraState.activateNewCamera(selectedObject);
                if(ObjConditionScript.script_checkAC != null)
                {
                    ObjConditionScript.script_checkAC.activateACCollider();
                }

                if (selectedObject.GetComponent<ConsoleBehaviourScript>() != null)
                {
                    script_console = selectedObject.GetComponent<ConsoleBehaviourScript>();
                }

                activateSecCollider();

                if(selectedObject.GetComponent<BedLampActivateScript>() != null)
                {
                    script_objToActivate = selectedObject.GetComponent<BedLampActivateScript>();
                }
                break;
            case "secondLevel":
                print($"tag : {Tag}");
                script_cameraState.activateNewCamera(selectedObject);
                if (ObjConditionScript.script_checkAC != null)
                {
                    ObjConditionScript.script_checkAC.activateACCollider();
                }

                activateSecCollider();

                if (selectedObject.GetComponent<BedLampActivateScript>() != null)
                {
                    script_objToActivate = selectedObject.GetComponent<BedLampActivateScript>();
                }
                break;
            case "plusButton":
                print($"tag : {Tag}");
                ObjConditionScript.script_checkAC.changeACValue(1);
                break;
            case "minButton":
                print($"tag : {Tag}");
                ObjConditionScript.script_checkAC.changeACValue(-1);
                break;
            case "Switch":
                print($"tag : {Tag}");

                if(selectedObject.GetComponentInParent<CheckACValueScript>() != null)
                {
                    script_objCondition.objACSwitch(selectedObject);
                    if(selectedObject.GetComponentInParent<ConsoleMiddleScript>() != null)
                    {
                        Debug.Log("Console Switch Hit!");
                        ConsoleMiddleScript script_console = selectedObject.GetComponentInParent<ConsoleMiddleScript>();
                        script_console.consoleCheck();
                    }
                } 
                else
                {
                    Debug.Log("Regular Switch Hit!");
                    script_objCondition.objSwitch(selectedObject);
                    script_cpuButton.CPUButtonPressed();
                    if(script_lampButton != null)
                    {
                        script_lampButton.LampButtonPressed();
                    }

                }
                break;
            case "Electricity":
                print($"tag : {Tag}");
                script_objCondition.eleButton();
                break;
            case "changeScene":
                print($"tag : {Tag}");
                selectedObject.GetComponent<DoorSceneScript>().changeRoom();
                script_lampButton = FindAnyObjectByType<LampButtonScript>();
                break;
            case "backScene":
                print($"tag : {Tag}");
                break;
            case "Selectable":
                print($"tag : {Tag}");
                if (selectedObject.GetComponent<ACTextWarningScript>() != null)
                {
                    StartCoroutine(ObjConditionScript.script_checkAC.acPopUpWarning());
                }
                break;
            case "Channel":
                print($"tag : {Tag}");
                if(selectedObject.GetComponent<TVRemoteScript>() != null)
                {
                    TVRemoteScript script_tv = selectedObject.GetComponent<TVRemoteScript>();
                    script_tv.changeChannel(script_tv.channel);
                    script_tv.animActivate(script_tv.channel);
                }
                break;
            case "Coffee":
                print($"tag : {Tag}");
                if (selectedObject.GetComponent<CoffeeScript>() != null)
                {
                    CoffeeScript script_coffee = selectedObject.GetComponent<CoffeeScript>();
                    script_coffee.isCupPlaced = true;
                }
                break;
            case "Console":
                print($"tag : {Tag}");
                if (selectedObject.GetComponentInParent<ConsoleMiddleScript>() != null)
                {
                    ConsoleMiddleScript script_console = selectedObject.GetComponentInParent<ConsoleMiddleScript>();
                    script_console.consoleOn();
                    script_console.consoleCheck();
                }

                if (script_console != null)
                {
                    script_console.ConsoleButtonPressed();
                }
                break;
            default:
                print($"tag : {Tag}");
                break;
        }
    }

    private bool isTouchOverUI = false;

    private void touchHandler()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // Assuming you're interested in the first touch

            if (touch.phase == TouchPhase.Began)
            {
                if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                {
                    // Do not process the raycast if the touch is over a UI element
                    Debug.Log("UI Block Raycast");
                    isTouchOverUI = true;
                    return;
                }
                else
                {
                    isTouchOverUI = false;
                    Ray ray = Camera.main.ScreenPointToRay(touch.position);
                    RaycastHit hit;
                    startTouchPos = touch.position;

                    if (Physics.Raycast(ray, out hit))
                    {
                        GameObject touchedObject = hit.transform.gameObject;

                        selectedObject = touchedObject;
                        print($"hit {touchedObject.tag}");

                        if (selectedObject.GetComponentInParent<CheckACValueScript>() != null)
                        {
                            ObjConditionScript.script_checkAC = selectedObject.GetComponentInParent<CheckACValueScript>();
                        }

                        if (selectedObject.GetComponent<SoundEffectScript>() != null)
                        {
                            SoundEffectScript sfx = selectedObject.GetComponent<SoundEffectScript>();
                            sfx.playSound();
                        }

                        select(touchedObject.tag);
                    }
                }
            }

            if (isTouchOverUI)
            {
                // Do not process the rest of the touch phases if it started over a UI element
                return;
            }

            if (touch.phase == TouchPhase.Moved && script_cameraState.currentVirtualCamera.CompareTag("firVirtualCamera"))
            {
                if (script_objToActivate != null)
                    script_objToActivate.obj.transform.Rotate(0, -touch.deltaPosition.x * script_objToActivate.turnspeed * Time.deltaTime, 0);
            }

            if (touch.phase == TouchPhase.Ended)
            {
                endTouchPos = touch.position;
                if (script_cameraState.currentVirtualCamera.CompareTag("mainVirtualCamera"))
                {
                    if (startTouchPos.y < Screen.height / 2)
                    {
                        if (endTouchPos.x - startTouchPos.x < -100)
                        {
                            Debug.Log("Next Swipe");
                            script_cameraState.nextSwipe();
                        }

                        if (endTouchPos.x - startTouchPos.x > 100)
                        {
                            Debug.Log("Prev Swipe");
                            script_cameraState.prevSwipe();
                        }
                    }
                }
            }
        }
    }


}
