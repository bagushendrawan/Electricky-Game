using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class gameState : MonoBehaviour
{
    private singletonData script_Data;
    private Canvas winCanvas;
    private Canvas loseCanvas;

    public enum state
    {
        mainGame,
        pauseGame,
        winGame,
        loseGame,
        menuGame
    }

    private state currentState;

    void Start()
    {
        ChangeState(state.mainGame);
        script_Data = GetComponent<singletonData>();
        winCanvas = GameObject.Find("Canvas_Win").GetComponent<Canvas>();
        loseCanvas = GameObject.Find("Canvas_Lose").GetComponent<Canvas>();
        winCanvas.enabled = false;
        loseCanvas.enabled = false;
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
            case state.mainGame:
                break;
            case state.pauseGame:
                break;
            case state.winGame:
                winCanvas.enabled = true;
                break;
            case state.loseGame:
                loseCanvas.enabled = true;
                break;
            case state.menuGame:
                break;
        }
    }

    void ExitState()
    {
        switch (currentState)
        {
            case state.mainGame:
                break;
            case state.pauseGame:
                break;
            case state.winGame:
                break;
            case state.loseGame:
                break;
            case state.menuGame:
                break;
        }
    }

    void PerformStateActions()
    {
        switch (currentState)
        {
            case state.mainGame:
                //Debug.Log("On mainGame state");
                if(script_Data.win)
                {
                    ChangeState(state.winGame);
                }

                if (script_Data.lose)
                {
                    ChangeState(state.loseGame);
                }
                break;
            case state.pauseGame:
                //Debug.Log("On pauseGame state");
                break;
            case state.winGame:
                //Debug.Log("On winGame state");
                break;
            case state.loseGame:
                //Debug.Log("On loseGame state");
                break;
            case state.menuGame:
                //Debug.Log("On menuGame state");
                break;
        }
    }
}
