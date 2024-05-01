using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class uiButton : MonoBehaviour
{
    private dataHandler script_dataHandler;

    private void Start()
    {
        script_dataHandler = GetComponent<dataHandler>();
    }
    public void canvasToggle(Canvas canvas)
    {
        canvas.enabled = !canvas.enabled;
    }
    public void resumeButton(Canvas canvas)
    {
        canvas.enabled = !canvas.enabled;
        Time.timeScale = 1;
    }
    public void changeScene(int index)
    {
        SceneManager.LoadSceneAsync(index);
    }

    public void exitScene(int index)
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        SceneManager.LoadSceneAsync(index);
    }

    public void quit()
    {
        script_dataHandler.Save();
        Application.Quit();
    }
}
