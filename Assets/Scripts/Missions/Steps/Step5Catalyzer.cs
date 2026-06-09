using UnityEngine;

public class Step5Catalyzer : CatalyzerTrigger
{
    [Header("Step Pointers")]
    public MissionManager missionManager;

    public override void OnTrigger() {
        Debug.Log("Step5Catalyzer: step 5 completed");
        
        // missions
        missionManager.CompleteMission(5);
        
        if (!ReferenceSingleton.instance.consoleClick.CheckFinalMission()) {
        
        // doors
            // in case of lockUnreleatedDoor (don't need to check)
            ReferenceSingleton.instance.centerResearchDoor.UnlockingDoors();
        }

        if (SettingsStore.smartLockFinishedDoor)
        {
            ReferenceSingleton.instance.centerTechnicalDoor.LockingDoors();
        }
    }
}
