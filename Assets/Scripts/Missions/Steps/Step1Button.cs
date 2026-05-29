using UnityEngine;

public class Step1Button : ButtonTrigger
{
    public DoorOpeningScript doorScript;
    public MissionManager missionManager;

    public override void OnTrigger() {
        Debug.Log("Step1Button: step 1 completed");
        doorScript.OpeningDoors();
        missionManager.CompleteMission(1);
        missionManager.AddMission(2);
    }
}
