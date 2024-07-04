using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

//Should be a central game state, but only using it for win and lose
public class GameCondStateScript : MonoBehaviour
{
    private SingletonDataScript script_Data;
    private dataHandler script_dataHandler;
    private Canvas winCanvas;
    private Canvas loseCanvas;
    public GameObject[] starArray;

    public AudioSource sfx;
    public AudioClip win;
    public AudioClip lose;

    [SerializeField] private ScriptableObjectScript script_scriptable;
    [SerializeField] private TMP_Text winDesc;
    [SerializeField] private TMP_Text loseDesc;
    //public List<GameObject> starGameobject;

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
        script_dataHandler = GetComponent<dataHandler>();
        ChangeState(state.mainGame);
        script_Data = GetComponent<SingletonDataScript>();
        winCanvas = GameObject.Find("Canvas_Win").GetComponent<Canvas>();
        loseCanvas = GameObject.Find("Canvas_Lose").GetComponent<Canvas>();
        winCanvas.enabled = false;
        loseCanvas.enabled = false;

        foreach (KeyValuePair<int, int> kvp in dataHandler.starLoad)
        {
           Debug.Log(string.Format("Key = {0}, Value = {1}", kvp.Key, kvp.Value));
        }
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
                sfx.PlayOneShot(win);
                script_dataHandler.Load();

                int sceneIndex = SceneManager.GetActiveScene().buildIndex;
                int currentStar = calculateStars(script_scriptable.global_timer);
                Debug.Log("scene" + sceneIndex + "currentStar" + currentStar);
                for(int i=0; i<currentStar; i++)
                {
                    starArray[i].SetActive(true);
                }

                if(dataHandler.starLoad.ContainsKey(sceneIndex))
                {
                    if (dataHandler.starLoad[sceneIndex] < currentStar)
                    {
                        dataHandler.starLoad[sceneIndex] = currentStar;
                    }
                } else
                {
                    dataHandler.starLoad.Add(sceneIndex, currentStar);
                }


                winDesc.text = "Kamu Menghemat sebanyak " + Mathf.Floor(script_scriptable.global_eleQuota) + " kouta Listrik dalam waktu " + Mathf.Floor(SingletonDataScript.timer - script_scriptable.global_timer) + " detik";
                Time.timeScale = 0;
                winCanvas.enabled = true;
                script_dataHandler.Save();
                break;
            case state.loseGame:
                sfx.PlayOneShot(lose);
                Time.timeScale = 0;
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
                if(script_Data.global_win_Q)
                {
                    ChangeState(state.winGame);
                }

                if (script_Data.global_lose_Q)
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

    int calculateStars(float timeRemaining)
    {
        float star = timeRemaining;
        Debug.Log("star " + star);
        if(star > 180)
        {
            return 3;
        }

        if(star > 60)
        {
            return 2;
        }

        if(star > 0)
        {
            return 1;
        }

        return 0;
    }
}


