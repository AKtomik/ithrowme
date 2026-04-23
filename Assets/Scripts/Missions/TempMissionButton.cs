using UnityEngine;

public class TempMissionButton : MonoBehaviour
{
    public MissionManager missionManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other) {
        Debug.Log("colliding with "+ other.gameObject.layer);
        if (other.gameObject.layer != 10) return;
        missionManager.CompleteMission(0);
        missionManager.AddMission(1);
    }
}
