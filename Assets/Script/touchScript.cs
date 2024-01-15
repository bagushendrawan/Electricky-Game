using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class touchScript : MonoBehaviour
{
    private tagSelect script_tagSelect;
   [HideInInspector] public GameObject selectedObject;

    public int indexButton;

    void Start()
    {
        script_tagSelect = GetComponent<tagSelect>();
    }
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // Assuming you're interested in the first touch

            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    GameObject touchedObject = hit.transform.gameObject;
                    selectedObject = touchedObject;
                    print($"hit {touchedObject.tag}");

                    // Perform actions specific to the touchedObject
                    // For example, you can check its tag or name
                    if(touchedObject.CompareTag("Switch"))
                    {
                        selectObject();
                    }
                    script_tagSelect.select(touchedObject.tag);
                }
            }
        }
    }

    public scriptableObject scriptableScript;
    void selectObject()
    {
        scriptableScript.dataList[indexButton].taskStats = true;
    }
    
}
