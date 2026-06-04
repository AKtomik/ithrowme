using UnityEngine;

public class Step1Lever : TakableLever
{
    public MissionManager missionManager;
    public DoorOpeningScript doorScript;
    public GameObject activatedItem;

    public override void PullStart(CapsulePlayer player) {
        Debug.Log("Step1Lever: pulling...");
        missionManager.CompleteMission(1);
    }
    
    public override void PullFinish(CapsulePlayer player) {
        Debug.Log("Step1Lever: step 1 completed");
        doorScript.OpeningDoors();
        missionManager.AddMission(2);
        activatedItem.SetActive(true);
    }
}
