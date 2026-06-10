using UnityEngine;

public class Step1Lever : TakableLever
{
    [SerializeField] private AudioSource musicAudio;
    [Header("Step Pointers")]
    public MissionManager missionManager;
    public GameObject activatedItem;

    private AudioSource[] alarmsAudio = new AudioSource[] {};
    [SerializeField] private AudioSource powerOn;
    [SerializeField] private AudioManager audioManager;

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
            {
                alarm.gameObject.GetComponent<AudioLowPassFilter>().cutoffFrequency = 381f;
                alarm.Play();
            }
                
        }
    }

    public void MusicStart()
    {
        musicAudio.volume = 0.22f;
        musicAudio.Play();
        
        if (alarmsAudio != null && alarmsAudio.Length > 0)
        {
            foreach (AudioSource alarm in alarmsAudio)
                alarm.gameObject.GetComponent<AudioLowPassFilter>().enabled = false;
                    
        }
        
    }


    public override void PullStart(CapsulePlayer player) {
        Debug.Log("Step1Lever: pulling...");
        missionManager.CompleteMission(1);
        

    }
    
    public override void PullFinish(CapsulePlayer player) {
        Debug.Log("Step1Lever: step 1 completed");
        powerOn.Play();

        if (alarmsAudio != null && alarmsAudio.Length > 0)
        {
            foreach (AudioSource alarm in alarmsAudio)
                alarm.Stop();
        }
        musicAudio.Stop();
        ReferenceSingleton.instance.emergencyLifeDoor.OpeningDoors();
        missionManager.AddMission(2);
        activatedItem.SetActive(true);
        audioManager.StartAmbiance();
    }


}
