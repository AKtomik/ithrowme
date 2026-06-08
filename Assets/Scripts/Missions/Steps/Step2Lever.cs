using UnityEngine;

public class Step2Lever : TakableLever
{
    [Header("Step Pointers")]
    public MissionManager missionManager;
    public ConsoleScreenManager consoleScreen;

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
    
    // animation wrappers
    public void AnimationAlias2Start()
    {
        consoleScreen.ClearTexts();
    }

    public void AnimationAlias2Waiting()
    {
        consoleScreen.SetPrintColor(Color.gold);
        consoleScreen.SetPrintColor(Color.black);
        consoleScreen.AddText("executing ejection process...", 1f);
    }
    
    public void AnimationAlias2Exe1()
    {
        consoleScreen.AddText("error: not authorized", .5f);
    }
    
    public void AnimationAlias2Exe2()
    {
        consoleScreen.AddText("error: no energy", .5f);
    }
    
    public void AnimationAlias2Failure()
    {
        consoleScreen.AddText("ejection failed", Color.white, 1f);
    }

    public void AnimationAlias2Ended()
    {
        AnimationEnded();
    }
}
