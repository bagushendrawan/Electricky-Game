using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraStatesScript : MonoBehaviour
{
    //save current active camera
    [HideInInspector] public CinemachineVirtualCamera currentVirtualCamera;

    //control camera level act like a stack, could be optimized with stack
    [HideInInspector] public List<CinemachineVirtualCamera> virCameraList;

    [HideInInspector] public int currentCameraIndex;
    [HideInInspector] public int currentState;

    [Header("Assign The Camera and Wall Sides")]
    public List<CinemachineVirtualCamera> roomCamera;
    public List<GameObject> roomSides;

    [Header("Wall Animation")]
   //[SerializeField] private float distance;
   //[SerializeField] private float speed;
    [SerializeField] private float fadeDuration;
    [SerializeField] private string animTriggerUp;
    [SerializeField] private string animTriggerDown;


    void Start()
    {
        deactivateAllCameras();
        virCameraList.Add(roomCamera[0]);
        currentVirtualCamera = virCameraList[0];
        currentCameraIndex = 0;
        activateMove(currentCameraIndex);
    }

    void Update()
    {

    }

    //activate obj camera
    public void activateNewCamera(GameObject selectedObject)
    {
        if(selectedObject.GetComponent<ObjCameraScript>().cam != virCameraList[virCameraList.Count - 1])
        {
            currentVirtualCamera.enabled = false;
            virCameraList.Add(selectedObject.GetComponent<ObjCameraScript>().cam);
            currentVirtualCamera = virCameraList[virCameraList.Count - 1];
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
        virCameraList.RemoveAt(virCameraList.Count - 1);
        currentVirtualCamera = virCameraList[virCameraList.Count - 1];
        currentVirtualCamera.enabled = true;
    }

    /// <summary>
    /// 
    /// </summary>
    public void nextSwipe()
    {
        deactivateAllCameras();
        if (currentVirtualCamera != roomCamera[3])
        {
            currentVirtualCamera.enabled = false;
            virCameraList.RemoveAt(virCameraList.Count - 1);
            roomCamera[currentCameraIndex + 1].enabled = true;
            virCameraList.Add(roomCamera[currentCameraIndex + 1]);
            currentVirtualCamera = virCameraList[virCameraList.Count - 1];
            currentCameraIndex = currentCameraIndex + 1;
        }
        else
        {
            currentVirtualCamera.enabled = false;
            virCameraList.RemoveAt(virCameraList.Count - 1);
            roomCamera[0].enabled = true;
            virCameraList.Add(roomCamera[0]);
            currentVirtualCamera = virCameraList[virCameraList.Count - 1];
            currentCameraIndex = 0;
        }
        activateMove(currentCameraIndex);
    }

    public void prevSwipe()
    {
        deactivateAllCameras();
        
        if (currentVirtualCamera != roomCamera[0])
        {
            currentVirtualCamera.enabled = false;
            virCameraList.RemoveAt(virCameraList.Count - 1);
            roomCamera[currentCameraIndex - 1].enabled = true;
            virCameraList.Add(roomCamera[currentCameraIndex - 1]);
            currentVirtualCamera = virCameraList[virCameraList.Count - 1];
            currentCameraIndex = currentCameraIndex - 1;
        }
        else
        {
            currentVirtualCamera.enabled = false;
            virCameraList.RemoveAt(virCameraList.Count - 1);
            roomCamera[3].enabled = true;
            virCameraList.Add(roomCamera[3]);
            currentVirtualCamera = virCameraList[virCameraList.Count - 1];
            currentCameraIndex = 3;
        }
        activateMove(currentCameraIndex);
    }

    void moveObjDown(GameObject obj, float duration, float pos)
    {
        Animator anim = obj.GetComponent<Animator>();
        if (obj.transform.position.y > 0)
        {
            anim.SetTrigger(animTriggerDown);
            StartCoroutine(DelayedExecution(obj, true, duration, pos));
        }
        
    }

    void moveObjUp(GameObject obj, float duration, float pos)
    {
        Animator anim = obj.GetComponent<Animator>();
        if (obj.transform.position.y <=0)
        {
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

    //private IEnumerator Start(GameObject obj, float trans)
    //{
    //    Renderer[] rend = obj.GetComponentsInChildren<Renderer>(true);
        
    //    foreach (Renderer renderer in rend)
    //    {
    //        Debug.Log(renderer.name);
    //        StartCoroutine(FadeOut(renderer, trans));
    //    }

    //    yield return new WaitForSeconds(fadeDuration);
    //}


    //IEnumerator FadeOut(Renderer rend, float trans)
    //{
    //    Material material = rend.material;
    //    Color originalColor = material.color;
    //    Color targetColor = new Color(originalColor.r, originalColor.g, originalColor.b, trans);

    //    float elapsed = 0f;
    //    while (elapsed < fadeDuration)
    //    {
    //        float alpha = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);
    //        material.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
    //        elapsed += Time.deltaTime;
    //        yield return null;
    //    }

    //    material.color = targetColor;
    //}

    void deactivateAllCameras()
    {
        for(int i = 0; i < roomCamera.Count; i++)
        {
            if (currentVirtualCamera != roomCamera[i])
            {
                roomCamera[i].enabled = false;
            }
            else
            {
                currentCameraIndex = i;
                Debug.Log("Current Cam Index :" + currentCameraIndex);
            }
        }
    }

    public void activateMove(int tag)
    {
        //Debug.Log("Move");
        switch(tag)
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

    //activate obj collider after switch cam
    void activateSwitch(GameObject selectedObject, bool col)
    {
        Collider[] objectToActivate = selectedObject.GetComponentsInChildren<Collider>(true);
        foreach (Collider x in objectToActivate)
        {
            if(x.gameObject.name == "Switch")
            {
                x.enabled = col;
            }
        }
    }
}
