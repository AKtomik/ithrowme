using UnityEngine;

public class Step1Lever : TakableLever
{
    [Header("Step Pointers")]
    public MissionManager missionManager;
    public GameObject activatedItem;

    private AudioSource[] alarmsAudio;

    private void Awake()
    {
        GameObject[] alarmObjects = GameObject.FindGameObjectsWithTag("Alarm");

        if (alarmObjects.Length > 0 )
        {
            var sources = new System.Collections.Generic.List<AudioSource>();
            foreach (GameObject obj in alarmObjects)
                sources.AddRange(obj.GetComponents<AudioSource>());

            alarmsAudio = sources.ToArray();

            foreach (AudioSource alarm in alarmsAudio)
                alarm.Play();
        }

    }
    public override void PullStart(CapsulePlayer player) {
        Debug.Log("Step1Lever: pulling...");
        missionManager.CompleteMission(1);


    }
    
    public override void PullFinish(CapsulePlayer player) {
        Debug.Log("Step1Lever: step 1 completed");
        if (alarmsAudio.Length > 0)
        {
            foreach (AudioSource alarm in alarmsAudio)
                alarm.Stop();
        }

        ReferenceSingleton.instance.emergencyLifeDoor.OpeningDoors();
        missionManager.AddMission(2);
        activatedItem.SetActive(true);
    }
}
