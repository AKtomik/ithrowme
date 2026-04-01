using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public Dictionary<int, string> missionsList = new Dictionary<int, string>(); // list of all the missions
    public ChangeMissionScript missionScript; // put in editor
    public int missionState = 0;

    public void NextMission()
    {
        missionState++;
        missionScript.changeMission(missionsList[missionState]);
    }

    private void Start()
    {
        missionsList.Add(1, "Réparer le générateur");
        NextMission();
    }
}
