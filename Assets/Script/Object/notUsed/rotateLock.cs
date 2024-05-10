using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateLock : MonoBehaviour
{
    private checkLock script_check;
    public GameObject firstLock;
    public GameObject secondLock;
    public GameObject thirdLock;
    public GameObject firstTrigger;
    public GameObject secondTrigger;
    public GameObject thirdTrigger;
    public GameObject currentLock;
    public GameObject checkLock;
    public bool gameActive;
    public bool gameResult;

    private void Start()
    {
        script_check = GetComponent<checkLock>();
        ChangeState(state.first);
    }
    void rotate(GameObject obj, Vector3 dir, float power)
    {
        obj.transform.Rotate(dir * power * Time.deltaTime);
    }

    public void press()
    {
        if(CheckIntersection(checkLock, currentLock))
        {
            if(currentState == state.first)
            {
                ChangeState(state.second);
            }
            else if (currentState == state.second)
            {
                ChangeState(state.third);
            }
            else if (currentState == state.third)
            {
                ChangeState(state.end);
            }
        }
        else
        {
            ChangeState(state.first);
        }    
    }

    bool CheckIntersection(GameObject obj1, GameObject obj2)
    {
        Collider collider1 = obj1.GetComponent<Collider>();
        Collider collider2 = obj2.GetComponent<Collider>();

        if (collider1 != null && collider2 != null)
        {
            return collider1.bounds.Intersects(collider2.bounds);
        }

        return false;
    }

    private state currentState;
    public enum state
    {
        first,
        second,
        third,
        end,
    }

    void Update()
    {
        PerformStateActions();
        
    }

    public void ChangeState(state newState)
    {
        ExitState();
        currentState = newState;
        EnterState();
    }

    void EnterState()
    {
        switch (currentState)
        {
            case state.first:
                Debug.Log("Entering first state");
                currentLock = firstTrigger;
                break;
            case state.second:
                Debug.Log("Entering second state");
                currentLock = secondTrigger;
                break;
            case state.third:
                Debug.Log("Entering third state");
                currentLock = thirdTrigger;
                break;
            case state.end:
                Debug.Log("Entering end state");
                break;
        }
    }

    void ExitState()
    {
        switch (currentState)
        {
            case state.first:
                Debug.Log("Exiting first state");
                break;
            case state.second:
                Debug.Log("Exiting second state");
                break;
            case state.third:
                Debug.Log("Exiting third state");
                break;
            case state.end:
                Debug.Log("Exiting end state");
                break;
        }
    }

    void PerformStateActions()
    {
        switch (currentState)
        {
            case state.first:
                Debug.Log("On first state");
                rotate(firstLock, Vector3.up, 180);
                
                break;
            case state.second:
                Debug.Log("On second state");
                rotate(secondLock, Vector3.up, -200);
                break;
            case state.third:
                Debug.Log("On third state");
                rotate(thirdLock, Vector3.up, 180);
                break;
            case state.end:
                Debug.Log("On end state");
                gameResult = true;
                break;
        }
    }
}
