using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelUnlockScript : MonoBehaviour
{
    public dataHandler script_datahandler;
    public List<GameObject> levelButtonList;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < levelButtonList.Count; i++)
        {
            levelButtonList[i].SetActive(false);
        }

        for (int i = 0; i < dataHandler.unlockedScene+1; i++)
        {
            levelButtonList[i].SetActive(true);
        }
    }
}
