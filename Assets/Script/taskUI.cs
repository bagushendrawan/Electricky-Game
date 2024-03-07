using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class taskUI : MonoBehaviour
{
    public Dictionary<string, bool> task = new Dictionary<string, bool>();
    public scriptableObject script_Scriptable;
    private TMP_Text taskText;
    // Start is called before the first frame update
    void Start()
    {
        taskText = GameObject.Find("Task").GetComponent<TextMeshProUGUI>();
        showTask();
        updateTask();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void showTask()
    {
        foreach (var x in script_Scriptable.dataList)
        {
            task.Add(x.taskName, x.taskStats);
        }
    }
    public void updateTask()
    {
        taskText.text = null;
        foreach (var x in script_Scriptable.dataList)
        {
            task[x.taskName] = x.taskStats;
        }

        foreach (var pair in task)
        {
            taskText.text += $"{pair.Key} Stats: {pair.Value.ToString()}\n";
        }

        
    }
}
