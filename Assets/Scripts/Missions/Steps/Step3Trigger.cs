using UnityEngine;

public class Step3Trigger : MonoBehaviour
{
    [Header("Step Pointers")]
    public MissionManager missionManager;

    private void OnTriggerExit(Collider other) {
        if (!this.enabled || !other.gameObject.CompareTag("Player")) return;
        if (!missionManager.IsCompleted(3)) return;
        Debug.Log("Step3Trigger: step 3 closed");
        if (SettingsStore.smartLockFinishedDoor)
        {
            ReferenceSingleton.instance.centerResearchDoor.LockingDoors();
        }
        this.enabled = false;
    }
}
