using UnityEngine;

public class Step1Lever : TakableLever
{
    [SerializeField] private AudioSource musicAudio;
    [Header("Step Pointers")]
    [SerializeField] private MissionManager missionManager;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private Transform lookAwayPoint;
    [SerializeField] private Transform lookBackPoint;

    [SerializeField] private AudioSource powerOn;
    public void MusicStart()
    {
        Debug.Log("musicAudio start");
        musicAudio.volume = 0.22f;
        musicAudio.Play();
        audioManager.SetAlarmsFilter(false);
    }

    public override void PullStart(CapsulePlayer player) {
        Debug.Log("Step1Lever: pulling...");
        missionManager.CompleteMission(1);
    }
    
    public override void PullFinish(CapsulePlayer player) {
        Debug.Log("Step1Lever: step 1 completed");
        powerOn.Play();

        musicAudio.Stop();
        ReferenceSingleton.instance.emergencyLifeDoor.OpeningDoors();
        missionManager.AddMission(2);
        audioManager.StartAmbiance();
        audioManager.KillAlarms();
    }

    // animation
    public void AnimationAlias1LookAway()
    {
        AnimationLookingPoint(lookAwayPoint, .15f);
    }
    public void AnimationAlias1LookBack()
    {
        AnimationLookingPoint(lookBackPoint, .6f);
    }
}
