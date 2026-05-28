using UnityEngine;

public class Step1Lever : TakableLever
{
    public DoorOpeningScript doorScript;
    public MissionManager missionManager;
    public TakableItem enableItem;

    public override void OnTrigger(CapsulePlayer player) {
        Debug.Log("Step1Button: step 1 completed");
        doorScript.OpeningDoors();
        missionManager.CompleteMission(1);
        missionManager.AddMission(2);
        enableItem.enabled = true;
    }
}
