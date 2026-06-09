using UnityEngine;

public class Step5Catalyzer : CatalyzerTrigger
{
    public MissionManager missionManager;

    public override void OnTrigger() {
        Debug.Log("Step5Catalyzer: step 5 completed");
        
        // missions
        missionManager.CompleteMission(5);
        
        if (!ReferenceStore.instance.consoleClick.CheckFinalMission()) {
        
        // doors
            // in case of lockUnreleatedDoor (don't need to check)
            ReferenceStore.instance.centerResearchDoor.UnlockingDoors();
        }

        if (SettingsStore.smartLockFinishedDoor)
        {
            ReferenceStore.instance.centerTechnicalDoor.LockingDoors();
        }
    }
}
