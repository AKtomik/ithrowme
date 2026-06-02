using UnityEngine;

public class Step6Lever : TakableLever
{
    public MissionManager missionManager;

    public override void PullStart(CapsulePlayer player) {
        Debug.Log("Step7Lever: pulling...");
        missionManager.CompleteMission(7);
    }
    public override void PullFinish(CapsulePlayer player)
    {
        Debug.Log("Step7Lever: step 7 completed");
        //missionManager.AddMission(3);
    }
}
