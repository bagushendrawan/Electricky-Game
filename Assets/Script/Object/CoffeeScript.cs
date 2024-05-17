using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeScript : MonoBehaviour
{
    public GameObject objToDeactivate;
    public GameObject objToActivate;
   [HideInInspector] public bool isCupPlaced = false;
    public int coffeeIndex;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isCupPlaced)
        {
            ObjConditionScript.obj_dataList[coffeeIndex].tronic_correct_Q = true;
            objToActivate.SetActive(true);
            objToDeactivate.SetActive(false);
        } else
        {
            ObjConditionScript.obj_dataList[coffeeIndex].tronic_correct_Q = false;
        }
    }
}
