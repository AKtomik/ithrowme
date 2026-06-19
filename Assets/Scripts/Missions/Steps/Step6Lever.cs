using UnityEngine;

public class Step6Lever : TakableLever
{
    [Header("Step Pointers")]
    public MissionManager missionManager;
    public ConsoleScreenManager consoleScreen;
    public Animation hubloAnimation;
    
    [Header("Step Settings")]
    public bool animClearConsole = true;
    public bool animShowExe = true;
    
    public override bool PullCheck(CapsulePlayer player) {
        return missionManager.IsActive(6);
    }

    public override void PullStart(CapsulePlayer player) {
        Debug.Log("Step6Lever: pulling...");
        missionManager.CompleteMission(6);
    }
    public override void PullFinish(CapsulePlayer player)
    {
        Debug.Log("Step6Lever: step 6 completed");
        missionManager.AddMission(7);
        ReferenceSingleton.instance.centerControlDoor.detectPlayer = false;
        ReferenceSingleton.instance.centerControlDoor.LockingDoors();
        //missionManager.AddMission(3);
    }
    
    // animation wrappers
    public void AnimationAlias6Start()
    {
        if (animClearConsole) consoleScreen.ClearTexts();
    }

    public void AnimationAlias6Waiting()
    {
        consoleScreen.SetScreenColor(Color.yellow);
        consoleScreen.SetPrintColor(Color.black);
        consoleScreen.AddText("re-preparing ejection...", 1f);
    }
    
    public void AnimationAlias6Exe1()
    {
        if (animShowExe) consoleScreen.AddText("you are authorized", Color.darkGreen, .5f);
    }
    
    public void AnimationAlias6Exe2()
    {
        if (animShowExe) consoleScreen.AddText("enough energy", Color.darkGreen, .5f);
    }
    
    public void AnimationAlias6Success()
    {
        consoleScreen.SetScreenColor(Color.green);
        consoleScreen.AddText("ejection READY", Color.white, 1f);
        hubloAnimation.Play();
        //consoleScreen.AddText("You succeeded", Color.white, 1f);
        //consoleScreen.AddText("get in the godot capsule to proceed ejection", Color.black, 2f);
    }

    public void AnimationAlias6Ended()
    {
        //consoleScreen.AddText("Thanks for playing!", Color.white, 1f);
        AnimationEnded();
    }
}
