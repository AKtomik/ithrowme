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
            takeCollider.enabled = false;// will be renabled in CheckFinalMission
        }
        else if (missionManager.IsCompleted(3) && missionManager.IsCompleted(5))
        {
            Debug.Log("Step2Step6Click: taking step 6");
            step6Script.Take(player);
        }
    }
    
    public bool CheckFinalMission() {
        if (missionManager.IsCompleted(3) && missionManager.IsCompleted(5))
        {
            missionManager.AddMission(6);
            ReferenceSingleton.instance.consoleScreen.SetScreenColor(new Color(.8f, .8f, .8f));
            ReferenceSingleton.instance.consoleScreen.AddText(".", new Color(0f, 0f, 0f, 0f));
            ReferenceSingleton.instance.consoleScreen.AddText("waiting for input...", Color.white);
            takeCollider.enabled = true;
            return true;
        }
        return false;
    }
}
