using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public class saveSystem : MonoBehaviour
{
    public static void SavePlayer()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.rock";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData dataPlayer = new PlayerData();

        formatter.Serialize(stream, dataPlayer);
        stream.Close();
    }

    public static PlayerData LoadSave()
    {
        string path = Application.persistentDataPath + "/player.rock";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData dataPlayer = formatter.Deserialize(stream) as PlayerData;
            stream.Close();
            Debug.Log("File LOAD EXIST on" + path);
            return dataPlayer;
        }
        else
        {
            Debug.Log("File not found on" + path);
            return null;
        }
    }
}

[System.Serializable]
public class PlayerData
{
    public int unlockedScene = 1;
    public Dictionary<int, int> starDict = new();
    public PlayerData()
    {
        if(dataHandler.currentScene > unlockedScene)
        unlockedScene = dataHandler.currentScene;
        starDict = GameCondStateScript.starsLevel; 
    }
}
