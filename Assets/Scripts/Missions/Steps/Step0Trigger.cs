using UnityEngine;

public class Step0Trigger : MonoBehaviour
{
    [Header("Step Pointers")]
    public MissionManager missionManager;
    public DoorOpeningScript closingDoorScript;

    private void OnTriggerEnter(Collider other) {
        if (!this.enabled || !other.gameObject.CompareTag("Player")) return;
        if (!missionManager.IsActive(0)) return;
        Debug.Log("Step0Trigger: step 0 completed");
        closingDoorScript.ClosingDoors();
        missionManager.CompleteMission(0);
        missionManager.AddMission(1);
        this.enabled = false;
    }
}
