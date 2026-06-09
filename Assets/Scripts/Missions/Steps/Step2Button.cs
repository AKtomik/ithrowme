using UnityEngine;

public class Step2Button : ButtonTrigger
{
    [Header("Step Pointers")]
    public MissionManager missionManager;

    public override void OnTrigger() {
        Debug.Log("Step2Button: step 2 completed");
        missionManager.CompleteMission(2);
        missionManager.AddMission(3);
        missionManager.AddMission(5);
    }
}
