using UnityEngine;

public class Step2Lever : TakableLever
{
    public MissionManager missionManager;
    public DoorOpeningScript[] unlockedDoors;

    public override void PullStart(CapsulePlayer player) {
        Debug.Log("Step2Lever: pulling...");
        missionManager.CompleteMission(2);
    }
    public override void PullFinish(CapsulePlayer player)
    {
        Debug.Log("Step2Lever: step 2 completed");
        foreach(var door in unlockedDoors)
        {
            door.UnlockingDoors();
        }
        missionManager.AddMission(3);
        missionManager.AddMission(5);
    }
}
