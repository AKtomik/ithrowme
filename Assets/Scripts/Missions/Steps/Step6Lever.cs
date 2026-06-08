using UnityEngine;

public class Step6Lever : TakableLever
{
    [Header("Step Pointers")]
    public MissionManager missionManager;
    public ConsoleScreenManager consoleScreen;

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
    
    // animation wrappers
    public void AnimationAlias6Start()
    {
        //consoleScreen.ClearTexts();
    }

    public void AnimationAlias6Waiting()
    {
        consoleScreen.SetPrintColor(Color.gold);
        consoleScreen.SetPrintColor(Color.black);
        consoleScreen.AddText("executing ejection process...", 1f);
    }
    
    public void AnimationAlias6Exe1()
    {
        consoleScreen.AddText("error: not authorized", .5f);
    }
    
    public void AnimationAlias6Exe2()
    {
        consoleScreen.AddText("error: no energy", .5f);
    }
    
    public void AnimationAlias6Failure()
    {
        consoleScreen.AddText("ejection failed", Color.white, 1f);
    }

    public void AnimationAlias6Ended()
    {
        AnimationEnded();
    }
}
