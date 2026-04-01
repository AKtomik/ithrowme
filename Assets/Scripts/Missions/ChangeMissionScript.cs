using TMPro;
using UnityEngine;

public class ChangeMissionScript : MonoBehaviour
{
    // This script must be in the main canva //

    [SerializeField] TextMeshProUGUI MissionText; // put the mission placeholder here

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }


    public void changeMission(string missionName)
    {
        
        if (!(MissionText.gameObject.activeSelf))
        {
            MissionText.gameObject.SetActive(true);
        }

        MissionText.text = missionName;
    }

}
