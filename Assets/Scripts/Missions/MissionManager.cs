using UnityEngine;
using System.Collections.Generic;
using TMPro;
using CurvedUI;

public class MissionManager : MonoBehaviour
{
    public GameObject MissionActiveTextPrefab;// prefab with a TextMeshProUGUI
    public Transform MissionUiContainer;// vertical layout group
    public List<string> MissionsDefinition = new List<string>() { "mission 0 lore", "mission 1 lore" };
    public List<int> InitialMissions = new List<int>() { 0 };
    
    private Dictionary<int, string> missionsDictionnary = new Dictionary<int, string>(); // list of all the missions
    private List<int> activeMissions = new List<int>(); // list of the active missions (the ones who are displayed)
    private List<int> finishMissions = new List<int>(); // list of the completed missions

    private List<GameObject> spawnedTextMissions = new List<GameObject>(); // every missions displayed

    public void AddMission(int id)
    {
        if (!activeMissions.Contains(id)) // verify if the mission is already displayed
        {
            activeMissions.Add(id);
            RefreshMissions(); // add the id to the list and refresh the canva
        }
    }

    public void CompleteMission(int id) // remove the id from the list then refresh (mission disappears)
    {
        if (activeMissions.Contains(id))
        {
            activeMissions.Remove(id);
            if (!finishMissions.Contains(id))
            {
                finishMissions.Add(id);
            }
            RefreshMissions();
        }
    }
    
    public bool IsCompleted(int id)
    {
        return finishMissions.Contains(id);
    }
    
    public bool IsActive(int id)
    {
        return activeMissions.Contains(id);
    }
    
    public bool IsWaiting(int id)
    {
        return !(IsActive(id) || IsCompleted(id));
    }

    private List<string> GetActiveMissionTexts()
    {
        return GetTexts(activeMissions);
    }

    private List<string> GetFinishMissionTexts()
    {
        return GetTexts(finishMissions);
    }
    
    private List<string> GetTexts(List<int> missionsId)
    {
        List<string> texts = new List<string>();
        foreach (int id in missionsId)
        {
            if (missionsDictionnary.ContainsKey(id))
                texts.Add(missionsDictionnary[id]);
        }
        return texts; // for each id, return their equivalent mission texts 
    }

    public void RefreshMissions()
    {
        // destroy old prefab before create new ones
        foreach (GameObject obj in spawnedTextMissions)
            Destroy(obj);
        spawnedTextMissions.Clear();

        // Instancier un prefab par mission active
        foreach (string text in GetActiveMissionTexts())
        {
            GameObject instance = Instantiate(MissionActiveTextPrefab, MissionUiContainer); // for every active mission : instantiate
            instance.AddComponent<CurvedUIVertexEffect>(); // add curved ui effect
            instance.AddComponent<CurvedUITMP>();
            instance.GetComponentInChildren<TextMeshProUGUI>().text = text; // write text
            spawnedTextMissions.Add(instance); // add to spawnedMissions
        }
    }

    private void Start()
    {
        for (int i = 0; i < MissionsDefinition.Count; i++)
            missionsDictionnary.Add(i, MissionsDefinition[i]);
        
        foreach (int id in InitialMissions)
            AddMission(id);
    }
    
}