using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public class dataHandler : MonoBehaviour
{
    [HideInInspector] public static int unlockedScene;
    public static int currentScene = 0;
    public static Dictionary<int, int> starLoad = new();
    public levelUnlockScript script_levelUnlock;

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

    public void ResetData()
    {
        currentScene = 0;
        starLoad.Clear();
        saveSystem.SavePlayer();
        if (script_levelUnlock != null)
        script_levelUnlock.checkLevel();
    }

    public void Load()
    {
        PlayerData dataPlayer = saveSystem.LoadSave();
        unlockedScene = dataPlayer.unlockedScene;
        starLoad = dataPlayer.starDict;
    }
}
