using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class levelUnlockScript : MonoBehaviour
{
    public dataHandler script_datahandler;
    public List<GameObject> levelButtonList;
    public GameObject[] level1_star;
    public GameObject[] level2_star;
    public GameObject[] level3_star;
    public dataHandler script_dataHandler;
    public GameObject obj_reset;
    // Start is called before the first frame update
    void Start()
    {
        script_datahandler.Load();
        if (dataHandler.starLoad.ContainsKey(1))
        {
            obj_reset.GetComponent<Button>().interactable = true;
            for (int i = 0; i < dataHandler.starLoad[1]; i++)
            {
                level1_star[i].SetActive(true);
            }

            if (dataHandler.starLoad.ContainsKey(2))
            {
                obj_reset.GetComponent<Button>().interactable = true;
                for (int i = 0; i < dataHandler.starLoad[2]; i++)
                {
                    level2_star[i].SetActive(true);
                }
            }

            if (dataHandler.starLoad.ContainsKey(3))
            {
                obj_reset.GetComponent<Button>().interactable = true;
                for (int i = 0; i < dataHandler.starLoad[3]; i++)
                {
                    level3_star[i].SetActive(true);
                }
            }
        }
        else
        {
            obj_reset.GetComponent<Button>().interactable = false;
            for (int i = 0; i < levelButtonList.Count; i++)
            {
                levelButtonList[i].SetActive(false);
            }
        }

        for (int i = 0; i < levelButtonList.Count; i++)
        {
            levelButtonList[i].SetActive(false);
        }

        for (int i = 0; i <= dataHandler.unlockedScene; i++)
        {
            levelButtonList[i].SetActive(true);
        }
    }

    public void checkLevel()
    {
        script_datahandler.Load();

        for (int i = 0; i < levelButtonList.Count; i++)
        {
            levelButtonList[i].SetActive(false);
            level1_star[i].SetActive(false);
            level2_star[i].SetActive(false);
            level3_star[i].SetActive(false);
        }

        for (int i = 0; i <= dataHandler.unlockedScene; i++)
        {
            levelButtonList[i].SetActive(true);

        }

        obj_reset.GetComponent<Button>().interactable = false;
    }
}
