using UnityEngine;

public class Step1Lever : TakableLever
{
    [SerializeField] private AudioSource musicAudio;
    [Header("Step Pointers")]
    public MissionManager missionManager;
    public GameObject activatedItem;
    public Transform lookAwayPoint;
    public Transform lookBackPoint;

    [SerializeField] private AudioSource powerOn;
    [SerializeField] private AudioManager audioManager;
    public void MusicStart()
    {
        Debug.Log("musicAudio start");
        musicAudio.volume = 0.22f;
        musicAudio.Play();
        ReferenceSingleton.instance.soundManager.SetAlarmFilter(false);
    }

    public override void PullStart(CapsulePlayer player) {
        Debug.Log("Step1Lever: pulling...");
        missionManager.CompleteMission(1);
    }
    
    public override void PullFinish(CapsulePlayer player) {
        Debug.Log("Step1Lever: step 1 completed");
        powerOn.Play();

        ReferenceSingleton.instance.soundManager.StopAlarm();
        musicAudio.Stop();
        ReferenceSingleton.instance.emergencyLifeDoor.OpeningDoors();
        missionManager.AddMission(2);
        audioManager.StartAmbiance();
        if (activatedItem) activatedItem.SetActive(true);
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
