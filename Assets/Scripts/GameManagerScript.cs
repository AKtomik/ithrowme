using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public Dictionary<int, string> missionsList = new Dictionary<int, string>(); // list of all the missions
    public ChangeMissionScript missionScript; // put in editor

    
    private List<int> activeMissions = new List<int>(); // list of the active missions (the ones who are displayed)

    public void AddMission(int id)
    {
        if (!activeMissions.Contains(id)) // verify if the mission is already displayed
        {
            activeMissions.Add(id);
            missionScript.RefreshMissions(GetActiveMissionTexts()); // add the id to the list and refresh the canva
        }
    }

    public void CompleteMission(int id) // remove the id from the list then refresh (mission disappears)
    {
        if (activeMissions.Contains(id))
        {
            activeMissions.Remove(id);
            missionScript.RefreshMissions(GetActiveMissionTexts());
        }
    }

    private List<string> GetActiveMissionTexts()
    {
        List<string> texts = new List<string>();
        foreach (int id in activeMissions)
        {
            if (missionsList.ContainsKey(id))
                texts.Add(missionsList[id]);
        }
        return texts; // for each id, return their equivalent mission texts 
    }

    private void Start()
    {
        missionsList.Add(1, "Réparer le générateur");
        missionsList.Add(2, "Trouver le fusible");
        missionsList.Add(3, "Parler au gardien");

        // Démarrer avec 2 missions simultanées
        AddMission(1);
        AddMission(2);
    }
}