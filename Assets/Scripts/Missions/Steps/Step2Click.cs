using UnityEngine;

public class Step2Click : TakableClick
{
    [Header("Common pointers")]
    public MissionManager missionManager;
    public GameObject consoleScreen;
    

    [Header("Step 2")]
    [SerializeField] private Animation step2Animation;
    public DoorOpeningScript[] step2UnlockedDoors;
    
    [Header("Step 6")]
    [SerializeField] private Animation step6Animation;

    private CapsulePlayer consolePlayer;

    // common

    public override void Click(CapsulePlayer player) {
        Debug.Log("Step2Step6Click: clicking...");
        if (missionManager.IsCompleted(5) && missionManager.IsCompleted(3))
        {
            StartStep6(player);
        }
        else
        {
            StartStep2(player);
        }
    }

    // step 2

    private void StartStep2(CapsulePlayer player)
    {
        consolePlayer = player;
        Debug.Log("Step2Click: step 2 animating...");
        missionManager.CompleteMission(2);
        FinishStep2();// !
    }
    public void FinishStep2()
    {
        Debug.Log("Step2Lever: step 2 completed");
        foreach(var door in step2UnlockedDoors)
        {
            door.UnlockingDoors();
        }
        missionManager.AddMission(3);
        missionManager.AddMission(5);
    }

    // step 6
    
    private void StartStep6(CapsulePlayer player)
    {
        consolePlayer = player;
        Debug.Log("Step6Click: step 6 animating...");
        missionManager.CompleteMission(6);
        FinishStep6();// !
    }
    public void FinishStep6()
    {
        Debug.Log("Step6Click: step 6 completed");
    }

    
}
