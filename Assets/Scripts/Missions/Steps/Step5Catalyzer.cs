using UnityEngine;

public class Step5Catalyzer : CatalyzerTrigger
{
    public MissionManager missionManager;

    public override void OnTrigger() {
        Debug.Log("Step5Catalyzer: step 5 completed");
        missionManager.CompleteMission(5);
        if (missionManager.IsCompleted(3))
            missionManager.AddMission(6);
    }
}
