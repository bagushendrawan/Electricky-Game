using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TaskStatsUIScript : MonoBehaviour
{
    public Dictionary<string, string> global_taskUI = new Dictionary<string, string>();
    public ScriptableObjectScript script_scriptable;
    public TMP_Text taskText;
    // Start is called before the first frame update
    void Start()
    {
        taskStats();
        updateTask();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void taskStats()
    {
        foreach (var x in script_scriptable.global_tronicDataList)
        {
            string stats = "Null";
            if (x.tronic_active_Q && x.tronic_statsDone_Q)
            {
                stats = "Active & Done";
            }

            if (x.tronic_statsDone_Q && !x.tronic_active_Q)
            {
                stats = "Done";
            }

            if (x.tronic_active_Q && !x.tronic_statsDone_Q)
            {
                stats = "Active";
            }

            if (!x.tronic_active_Q && !x.tronic_statsDone_Q)
            {
                stats = "Off";
            }

            global_taskUI.Add(x.tronic_name, stats);
        }
    }

    public void updateTask()
    {
        taskText.text = null;
        foreach (var x in script_scriptable.global_tronicDataList)
        {
            string stats = "Null";
            if (x.tronic_active_Q && x.tronic_statsDone_Q)
            {
                stats = "Active & Done";
            }

            if (x.tronic_statsDone_Q && !x.tronic_active_Q)
            {
                stats = "Done";
            }

            if (x.tronic_active_Q && !x.tronic_statsDone_Q)
            {
                stats = "Active";
            }

            if (!x.tronic_active_Q && !x.tronic_statsDone_Q)
            {
                stats = "Off";
            }

            global_taskUI[x.tronic_name] = stats;
        }

        foreach (var pair in global_taskUI)
        {
            taskText.text += $"{pair.Key} Stats: {pair.Value}\n";
        }

        
    }
}
