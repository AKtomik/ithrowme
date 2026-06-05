using UnityEngine;

public class Step2Step6Click : TakableClick
{
    [Header("Step Pointers")]
    public MissionManager missionManager;
    [SerializeField] private Step2Lever step2Script;
    [SerializeField] private Step6Lever step6Script;

    public override void Click(CapsulePlayer player) {
        Debug.Log("Step2Step6Click: clicking...");
        if (missionManager.IsWaiting(3))
        {
            Debug.Log("Step2Step6Click: taking step 2");
            step2Script.Take(player);
        }
        else if (missionManager.IsCompleted(3) && missionManager.IsCompleted(5))
        {
            Debug.Log("Step2Step6Click: taking step 6");
            step6Script.Take(player);
        }
    }    
}
