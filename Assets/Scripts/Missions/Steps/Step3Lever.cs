using UnityEngine;

public class Step3Lever : TakableLever
{
    public MissionManager missionManager;

    public override void PullStart(CapsulePlayer player) {
        Debug.Log("Step3Lever: pulling...");
        missionManager.CompleteMission(3);
    }
    public override void PullFinish(CapsulePlayer player)
    {
        Debug.Log("Step3Lever: step 3 completed");
        if (missionManager.IsCompleted(5))
        {
            missionManager.AddMission(6);
            ReferenceStore.instance.consoleScreen.SetScreenColor(new Color(.8f, .8f, .8f));
            ReferenceStore.instance.consoleScreen.AddText(".", new Color(0f, 0f, 0f, 0f));
            ReferenceStore.instance.consoleScreen.AddText("waiting for input...", Color.white);
        }

        // ! will lock the player in
        //if (SettingsStore.lockFinishedDoor)
        //{
        //    ReferenceStore.instance.centerTechnicalDoor.LockingDoors();
        //}
    }
}
