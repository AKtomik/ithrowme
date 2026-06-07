using UnityEngine;

public class Step4Trigger : MonoBehaviour
{
    public MissionManager missionManager;

    private void OnTriggerEnter(Collider other) {
        if (!this.enabled || !other.gameObject.CompareTag("Player")) return;
        Debug.Log("Step4Trigger: step 4 completed");
        missionManager.CompleteMission(4);
        missionManager.AddMission(5);
        if (SettingsStore.smartLockUnreleatedDoor)
        {
            ReferenceStore.instance.centerResearchDoor.LockingDoors();
        }
        this.enabled = false;
    }
}
