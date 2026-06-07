using UnityEngine;

public class Step2Lever : TakableLever
{
    [Header("Step Pointers")]
    public MissionManager missionManager;

    public override void PullStart(CapsulePlayer player) {
        Debug.Log("Step2Lever: pulling...");
        missionManager.CompleteMission(2);
    }
    public override void PullFinish(CapsulePlayer player)
    {
        Debug.Log("Step2Lever: step 2 completed");
        
        // missions
        missionManager.AddMission(3);
        missionManager.AddMission(4);
        
        // doors
        ReferenceStore.instance.centerResearchDoor.UnlockingDoors();
        ReferenceStore.instance.centerTechnicalDoor.UnlockingDoors();
        if (SettingsStore.smartLockFinishedDoor)
        {
            ReferenceStore.instance.centerLifeDoor.LockingDoors();
        }
    }

    public void AnimationEndedAlias2()
    {
        AnimationEnded();
    }
}
