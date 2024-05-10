using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class dataHandler : MonoBehaviour
{
    public int unlockedScene;
    public int currentScene;
    // Start is called before the first frame update
    void Start()
    {
        Load();
    }

    // Update is called once per frame
    void Update()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;

        if (currentScene > unlockedScene)
        {
            unlockedScene++;

        }
    }

    public void Save()
    {
        saveSystem.SavePlayer(this);
        Debug.Log("Data Saved");
        Debug.Log(unlockedScene);

    }

    public void Load()
    {
        PlayerData dataPlayer = saveSystem.LoadSave();
        unlockedScene = dataPlayer.unlockedScene;
        Debug.Log("Load Game");

        Debug.Log(unlockedScene);
    }
}
