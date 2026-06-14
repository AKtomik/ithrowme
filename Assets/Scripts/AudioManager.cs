using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource ambianceSound;
    private AlarmScript[] alarmsScripts;

    // setup
    void Start()
    {
        alarmsScripts = FindObjectsByType<AlarmScript>(FindObjectsSortMode.None);
    }

    // ambiance music
    public void StartAmbiance()
    {
        Invoke("PlayAmbiance", 8f);
    }

    private void PlayAmbiance()
    {
        ambianceSound.Play();
        Invoke("PlayAmbiance", Random.Range(30f, 60f));
    }
        
    public void StopAmbiance()
    {
        CancelInvoke();
    }
    	
    // alarms management
    public void KillAlarms()
    {
        foreach (AlarmScript alarm in alarmsScripts)
            alarm.KillAlarm();
    }

    public void SetAlarmsFilter(bool enabled = false)
    {
        foreach (AlarmScript alarm in alarmsScripts)
            alarm.EnableFilter(enabled);
    }
}
