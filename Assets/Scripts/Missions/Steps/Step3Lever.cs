using UnityEngine;

public class Step3Lever : TakableLever
{
    [Header("Step Pointers")]
    public MissionManager missionManager;
    public GameObject activatedItem;

    public override void PullStart(CapsulePlayer player) {
        Debug.Log("Step3Lever: pulling...");
        missionManager.CompleteMission(3);
    }
    public override void PullFinish(CapsulePlayer player)
    {
        Debug.Log("Step3Lever: step 3 completed");
        ReferenceSingleton.instance.consoleClick.CheckFinalMission();
        if (activatedItem) activatedItem.SetActive(true);

        // ! will lock the player in
        // but because of backdoor this is not a problem
        if (SettingsStore.smartLockFinishedDoor)
        {
            ReferenceSingleton.instance.centerResearchDoor.LockingDoors();
        }
    }
}
