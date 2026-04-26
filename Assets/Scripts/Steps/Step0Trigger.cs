using UnityEngine;

public class Step0Trigger : MonoBehaviour
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
        if (!other.gameObject.CompareTag("Items")) return;
        Debug.Log("step 0 completed");
        missionManager.CompleteMission(0);
        missionManager.AddMission(1);
    }
}
