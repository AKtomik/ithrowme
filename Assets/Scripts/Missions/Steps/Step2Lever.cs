using UnityEngine;

public class Step2Lever : TakableLever
{
    [Header("Step Pointers")]
    public MissionManager missionManager;
    public ConsoleScreenManager consoleScreen;
    
    [Header("Step Settings")]
    public bool animClearConsole = true;
    public bool animShowExe = true;

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
        ReferenceSingleton.instance.centerResearchDoor.UnlockingDoors();
        ReferenceSingleton.instance.centerTechnicalDoor.UnlockingDoors();
        if (SettingsStore.smartLockFinishedDoor)
        {
            ReferenceSingleton.instance.centerLifeDoor.LockingDoors();
        }
    }
    
    // animation wrappers
    public void AnimationAlias2Start()
    {
        if (animClearConsole) consoleScreen.ClearTexts();
    }

    public void AnimationAlias2Waiting()
    {
        consoleScreen.SetScreenColor(Color.yellow);
        consoleScreen.SetPrintColor(Color.black);
        consoleScreen.AddText("preparing ejection...", 1f);
    }
    
    public void AnimationAlias2Exe1()
    {
        if (animShowExe)
        {
            consoleScreen.SetScreenColor(Color.gold);
            consoleScreen.AddText("error: not authorized", .6f);
        }
    }
    
    public void AnimationAlias2Exe2()
    {
        if (animShowExe)
        {
            consoleScreen.SetScreenColor(Color.gold);
            consoleScreen.AddText("error: no energy", .5f);
        }
    }
    
    public void AnimationAlias2Failure()
    {
        consoleScreen.SetScreenColor(Color.red);
        consoleScreen.SetPrintColor(Color.white);
        consoleScreen.AddText("ejection failed", 1f);
    }

    public void AnimationAlias2Ended()
    {
        AnimationEnded();
    }
}
