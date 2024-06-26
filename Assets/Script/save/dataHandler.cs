using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class dataHandler : MonoBehaviour
{
    [HideInInspector] public static int unlockedScene;
    public static int currentScene;
    public static Dictionary<int, int> starLoad = new();

    // Start is called before the first frame update
    void Start()
    {
        Load();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Save()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        saveSystem.SavePlayer();
        Debug.Log("Data Saved");
        Debug.Log(unlockedScene);
    }

    public void Load()
    {
        PlayerData dataPlayer = saveSystem.LoadSave();
        unlockedScene = dataPlayer.unlockedScene;
        starLoad = dataPlayer.starDict;
        Debug.Log("Load Game");
        Debug.Log(unlockedScene);
    }
}
