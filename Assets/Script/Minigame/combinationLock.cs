using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class combinationLock : MonoBehaviour
{
    [Header("Game Object")]
    [SerializeField] private GameObject light1;
    [SerializeField] private GameObject light2;
    [SerializeField] private GameObject light3;

    [Header("Combination")]
    [SerializeField] private List<char> list1;
    [SerializeField] private List<char> list2;
    [SerializeField] private List<char> list3;

    public List<char> listCheck = new List<char>();

    public void press1()
    {
        listCheck.Add('1');
    }
    public void press2()
    {
        listCheck.Add('2');
    }

    public void press3()
    {
        listCheck.Add('3');
    }

    private void Start()
    {
        ChangeState(state.first);
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
                ChangeObjectMaterial(light3, Color.white);
                ChangeObjectMaterial(light2, Color.white);
                ChangeObjectMaterial(light1, Color.yellow);
                break;
            case state.second:
                Debug.Log("Entering second state");
                ChangeObjectMaterial(light2, Color.yellow);
                break;
            case state.third:
                Debug.Log("Entering third state");
                ChangeObjectMaterial(light3, Color.yellow);
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
                ChangeObjectMaterial(light1, Color.green);
                break;
            case state.second:
                Debug.Log("Exiting second state");
                ChangeObjectMaterial(light2, Color.green);
                break;
            case state.third:
                Debug.Log("Exiting third state");
                ChangeObjectMaterial(light3, Color.green);
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
                if(listCheck.Count == 3)
                {
                    if(AreListsEqual(listCheck, list1))
                    {
                        Debug.Log("Correct Combination!");
                        listCheck.Clear();
                        ChangeState(state.second);
                    }
                    else
                    {
                        Debug.Log("Incorrect Combination!");
                        listCheck.Clear();
                    }
                }
                break;
            case state.second:
                Debug.Log("On second state");
                if (listCheck.Count == 3)
                {
                    if (AreListsEqual(listCheck, list2))
                    {
                        Debug.Log("Correct Combination!");
                        listCheck.Clear();
                        ChangeState(state.third);
                    }
                    else
                    {
                        Debug.Log("Incorrect Combination!");
                        listCheck.Clear();
                        ChangeState(state.first);
                    }
                }
                break;
            case state.third:
                Debug.Log("On third state");
                if (listCheck.Count == 3)
                {
                    if (AreListsEqual(listCheck, list3))
                    {
                        Debug.Log("Correct Combination!");
                        listCheck.Clear();
                        ChangeState(state.end);
                    }
                    else
                    {
                        Debug.Log("Incorrect Combination!");
                        listCheck.Clear();
                        ChangeState(state.first);
                    }
                }
                break;
            case state.end:
                Debug.Log("On end state");
                break;
        }
    }

    void ChangeObjectMaterial(GameObject target, Color color)
    {
        if (target != null)
        {
            Renderer renderer = target.GetComponent<Renderer>();

            if (renderer != null)
            {
                Material myMaterial = renderer.material;
                myMaterial.color = color;
            }
            else
            {
                Debug.LogError("Renderer component not found on the GameObject.");
            }
        }

    }

    public bool AreListsEqual(List<char> list1, List<char> list2)
    {
        if (list1.Count != list2.Count)
        {
            return false;
        }

        for (int i = 0; i < list1.Count; i++)
        {
            if (list1[i] != list2[i])
            {
                return false;
            }
        }

        return true;
    }
}
