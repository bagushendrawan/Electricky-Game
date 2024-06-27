using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelUnlockScript : MonoBehaviour
{
    public dataHandler script_datahandler;
    public List<GameObject> levelButtonList;
    public GameObject[] level1_star;
    public GameObject[] level2_star;
    public GameObject[] level3_star;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("load star3 " + dataHandler.starLoad[3]);
        Debug.Log("load scene " + dataHandler.unlockedScene);

        if (dataHandler.starLoad.ContainsKey(1))
        {
            for (int i = 0; i < dataHandler.starLoad[1]; i++)
            {
                level1_star[i].SetActive(true);
            }
        }

        if (dataHandler.starLoad.ContainsKey(2))
        {
            for (int i = 0; i < dataHandler.starLoad[2]; i++)
            {
                level2_star[i].SetActive(true);
            }
        }

        if (dataHandler.starLoad.ContainsKey(3))
        {
            for (int i = 0; i < dataHandler.starLoad[3]; i++)
            {
                level3_star[i].SetActive(true);
            }
        }

        for (int i = 0; i < levelButtonList.Count; i++)
        {
            levelButtonList[i].SetActive(false);
        }

        for (int i = 0; i < dataHandler.unlockedScene; i++)
        {
            levelButtonList[i].SetActive(true);
        }
    }
}
