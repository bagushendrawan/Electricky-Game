using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TaskStatsUIScript : MonoBehaviour
{
    public Dictionary<string, (GameObject checklist, GameObject stats)> global_taskUI = new Dictionary<string, (GameObject, GameObject)>();
    public ScriptableObjectScript script_scriptable;
    public TMP_Text taskText;
    public GameObject checklistPrefab; // Prefab for checklist items
    public Transform prefabParents;
    public Vector3 positionOffset;

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
        int i = 0;
        foreach (var x in script_scriptable.global_tronicDataList)
        {
            // Create a new checklist item
            GameObject checklistItem = Instantiate(checklistPrefab, prefabParents);
            GameObject stats = checklistItem; // Assuming stats is part of the checklistItem
            Vector3 newPosition = positionOffset * i++;

            // Set the position
            RectTransform rectTransform = checklistItem.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                rectTransform.anchoredPosition = newPosition;
            }

            if (x.tronic_statsDone_Q)
            {
                stats.SetActive(true);
            }
            else
            {
                stats.SetActive(false);
            }

            // Add to the dictionary with the checklist item and its stats
            global_taskUI.Add(x.tronic_name, (checklistItem, stats));
        }
    }

    public void updateTask()
    {
        taskText.text = null;
        foreach (var x in script_scriptable.global_tronicDataList)
        {
            // Unpack the tuple
            var (checklist, stats) = global_taskUI[x.tronic_name];

            if (x.tronic_statsDone_Q)
            {
                stats.SetActive(true);
            }
            else
            {
                stats.SetActive(false);
            }

            // Update the dictionary with the modified stats if needed
            global_taskUI[x.tronic_name] = (checklist, stats);
        }

        foreach (var pair in global_taskUI)
        {
            taskText.text += $"{pair.Key} Stats:\n";
        }
    }
}
