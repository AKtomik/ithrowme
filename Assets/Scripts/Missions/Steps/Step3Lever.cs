using UnityEngine;

public class Step3Lever : TakableLever
{
    [Header("Step Pointers")]
    public MissionManager missionManager;

    public override void PullStart(CapsulePlayer player) {
        Debug.Log("Step3Lever: pulling...");
        missionManager.CompleteMission(3);
    }
    public override void PullFinish(CapsulePlayer player)
    {
        Debug.Log("Step3Lever: step 3 completed");
        ReferenceSingleton.instance.consoleClick.CheckFinalMission();

        // ! will lock the player in
        //if (SettingsStore.lockFinishedDoor)
        //{
        //    ReferenceStore.instance.centerTechnicalDoor.LockingDoors();
        //}
    }
}
