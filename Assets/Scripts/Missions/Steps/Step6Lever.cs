using UnityEngine;

public class Step6Lever : TakableLever
{
    [Header("Step Pointers")]
    public MissionManager missionManager;

    public override void PullStart(CapsulePlayer player) {
        Debug.Log("Step6Lever: pulling...");
        missionManager.CompleteMission(6);
    }
    public override void PullFinish(CapsulePlayer player)
    {
        Debug.Log("Step6Lever: step 6 completed");
        TimerScript.instance.EndTime();
        //missionManager.AddMission(3);
    }
    
    public void AnimationEndedAlias6()
    {
        AnimationEnded();
    }
}
