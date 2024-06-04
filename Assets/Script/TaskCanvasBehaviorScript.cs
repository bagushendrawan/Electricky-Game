using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskCanvasBehaviorScript : MonoBehaviour
{
    private Canvas taskCanvas;

    private void Start()
    {
        taskCanvas = GetComponent<Canvas>();
    }

    public void CloseCanvas()
    {
        taskCanvas.enabled = false;
    }

    public void OpenCanvas()
    {
        taskCanvas.enabled = true;
    }
}
