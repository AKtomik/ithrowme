using UnityEngine;

public class Step0Trigger : MonoBehaviour
{
    public MissionManager missionManager;
    public DoorOpeningScript closingDoorScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        //Debug.Log("step 1 colliding with "+ other.gameObject.layer);
        if (!other.gameObject.CompareTag("Player")) return;
        Debug.Log("Step0Trigger: step 0 completed");
        closingDoorScript.ClosingDoors();
        missionManager.CompleteMission(1);
        missionManager.AddMission(2);
        this.enabled = false;
    }
}
