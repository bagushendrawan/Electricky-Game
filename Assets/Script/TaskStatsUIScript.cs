using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TaskStatsUIScript : MonoBehaviour
{
    public Dictionary<string, bool> global_taskUI = new Dictionary<string, bool>();
    public ScriptableObjectScript script_scriptable;
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
        foreach (var x in script_scriptable.global_tronicDataList)
        {
            global_taskUI.Add(x.tronic_name, x.tronic_statsDone_Q);
        }
    }
    public void updateTask()
    {
        taskText.text = null;
        foreach (var x in script_scriptable.global_tronicDataList)
        {
            global_taskUI[x.tronic_name] = x.tronic_statsDone_Q;
        }

        foreach (var pair in global_taskUI)
        {
            taskText.text += $"{pair.Key} Stats: {pair.Value.ToString()}\n";
        }

        
    }
}
