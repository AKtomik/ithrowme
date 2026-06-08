using UnityEngine;

public class Step5Catalyzer : CatalyzerTrigger
{
    public MissionManager missionManager;

    public override void OnTrigger() {
        Debug.Log("Step5Catalyzer: step 5 completed");
        
        // missions
        missionManager.CompleteMission(5);
        if (missionManager.IsCompleted(3))
        {
            missionManager.AddMission(6);
            ReferenceStore.instance.consoleScreen.SetScreenColor(new Color(.8f, .8f, .8f));
            ReferenceStore.instance.consoleScreen.AddText(" ", Color.black);
            ReferenceStore.instance.consoleScreen.AddText("waiting for input...", Color.white);
        }
        
        // doors
        else {
            // in case of lockUnreleatedDoor (don't need to check)
            ReferenceStore.instance.centerResearchDoor.UnlockingDoors();
        }

        if (SettingsStore.smartLockFinishedDoor)
        {
            ReferenceStore.instance.centerTechnicalDoor.LockingDoors();
        }
    }
}
