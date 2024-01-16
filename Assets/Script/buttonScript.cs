using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonScript : MonoBehaviour
{
    public scriptableObject scriptableScript;
    public singletonStats stats;
    public uiScript script_ui;
    public int indexButton;
    public bool buttonOpen;

    private Collider objectCollider;

    void Awake()
    {
        buttonOpen = scriptableScript.dataList[indexButton].taskOpen;
        stats = FindAnyObjectByType<singletonStats>().GetComponent<singletonStats>();
        script_ui = FindAnyObjectByType<uiScript>().GetComponent<uiScript>();
    }

    private void Update()
    {
        objectCollider = GetComponent<Collider>();
        objectCollider.enabled = buttonOpen;
    }
    public void selectObject()
    {
        if(scriptableScript.dataList[indexButton].taskStats != true)
        {
            scriptableScript.dataList[indexButton].taskStats = true;
            decreaseEleCapacity();
        }
        else
        {
            scriptableScript.dataList[indexButton].taskStats = false;
            increaseEleCapacity();
        }
    }

    public void decreaseEleCapacity()
    {
        stats.eleCapacity -= scriptableScript.dataList[indexButton].wattage;
        script_ui.updateWattageUI();
    }

    public void increaseEleCapacity()
    {
        stats.eleCapacity += scriptableScript.dataList[indexButton].wattage;
        script_ui.updateWattageUI();
    }
}
