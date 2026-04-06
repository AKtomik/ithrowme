using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChangeMissionScript : MonoBehaviour
{
    public GameObject MissionPrefab;       // prefab with a TextMeshProUGUI
    public Transform MissionContainer;    // vertical layout group

    private List<GameObject> spawnedMissions = new List<GameObject>(); // every missions displayed

    public void RefreshMissions(List<string> missionTexts)
    {
        // destroy old prefab before create new ones
        foreach (GameObject obj in spawnedMissions)
            Destroy(obj);
        spawnedMissions.Clear();

        // Instancier un prefab par mission active
        foreach (string text in missionTexts)
        {
            GameObject instance = Instantiate(MissionPrefab, MissionContainer); // for every active mission : instantiate
            instance.GetComponentInChildren<TextMeshProUGUI>().text = text; // write text
            spawnedMissions.Add(instance); // add to spawnedMissions
        }
    }
}