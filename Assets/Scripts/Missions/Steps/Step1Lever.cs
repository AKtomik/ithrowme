using UnityEngine;

public class Step1Lever : TakableLever
{
    [Header("Step Pointers")]
    [SerializeField] private MissionManager missionManager;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private Transform lookAwayPoint;
    [SerializeField] private Transform lookBackPoint;
    [SerializeField] private Animation parallaxEjectionAnimation;

    [SerializeField] private AudioSource powerOn;

    public override void PullStart(CapsulePlayer player) {
        Debug.Log("Step1Lever: pulling...");
        missionManager.CompleteMission(1);
    }
    
    public override void PullFinish(CapsulePlayer player) {
        Debug.Log("Step1Lever: step 1 completed");
        powerOn.Play();

        ReferenceSingleton.instance.emergencyLifeDoor.OpeningDoors();
        ReferenceSingleton.instance.centerLifeDoor.UnlockingDoors();
        missionManager.AddMission(2);
        audioManager.StopRunMusic();
        audioManager.StartAmbianceMusic();
        audioManager.KillAlarms();
    }

    // animation
    public void AnimationAlias1LookAway()
    {
        AnimationLookingPoint(lookAwayPoint, .15f);
    }
    
    public void AnimationAlias1Eject()
    {
        parallaxEjectionAnimation.Play();
    }

    public void AnimationAlias1LookBack()
    {
        AnimationLookingPoint(lookBackPoint, .6f);
    }
}
