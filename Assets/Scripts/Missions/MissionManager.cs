using UnityEngine;
using System.Collections.Generic;
using TMPro;
using CurvedUI;
using System;

public class MissionManager : MonoBehaviour
{
    public GameObject MissionActiveTextPrefab;// prefab with a TextMeshProUGUI
    public Transform MissionUiContainer;// vertical layout group
    [Obsolete] public List<string> MissionsDefinition = new() { "mission 0 lore", "mission 1 lore" };
    public List<string> MissionsIndication = new() { "mission 0 indication", "mission 1 lore" };
    public List<string> MissionsLocation = new() { "mission 0 lore", "mission 1 lore" };
    public List<int> InitialMissions = new() { 0 };
    
    private Dictionary<int, MissionData> missionsDictionnary = new(); // list of all the missions
    private List<int> activeMissions = new(); // list of the active missions (the ones who are displayed)
    private List<int> finishMissions = new(); // list of the completed missions

    private Dictionary<int, MissionIndicator> spawnedTextMissions = new(); // every missions displayed

    public void AddMission(int id)
    {
        if (!activeMissions.Contains(id)) // verify if the mission is already displayed
        {
            activeMissions.Add(id);
            SpawnMissionText(id); // add the id to the list and refresh the canva
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
            DespawnMissionText(id);
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

    private void SpawnMissionText(int id)
    {
        GameObject instance = Instantiate(MissionActiveTextPrefab, MissionUiContainer); // for every active mission : instantiate
        MissionIndicator indicator = instance.GetComponent<MissionIndicator>();
        indicator.PutData(missionsDictionnary[id]);
        spawnedTextMissions[id] = indicator; // add to spawnedMissions
    }
    
    private void DespawnMissionText(int id)
    {
        var indicator = spawnedTextMissions[id];
        indicator.Kill();
    }

    private void Start()
    {
        for (int i = 0; i < MissionsIndication.Count; i++)
        {
			missionsDictionnary.Add(i, 
    			new()
    			{
    				indicationText = MissionsIndication[i],
    				locationText = MissionsLocation[i]
    			}
            );
        }
        
        foreach (int id in InitialMissions)
            AddMission(id);
    }
    
}