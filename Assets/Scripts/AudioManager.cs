using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource ambianceMusic;
    [SerializeField] private AudioSource runMusic;
    private AlarmScript[] alarmsScripts;

    // setup
    void Start()
    {
        alarmsScripts = FindObjectsByType<AlarmScript>(FindObjectsSortMode.None);
    }

    // run music
    public void PlayRunMusic()
    {
        runMusic.volume = 0.22f;
        runMusic.Play();
    }
    
    public void StopRunMusic()
    {
        runMusic.Stop();
    }

    // ambiance music
    public void StartAmbianceMusic()
    {
        Invoke(nameof(PlayAmbianceNote), 8f);
    }

    private void PlayAmbianceNote()
    {
        ambianceMusic.Play();
        Invoke(nameof(PlayAmbianceNote), Random.Range(30f, 60f));
    }
        
    public void StopAmbianceMusic()
    {
        ambianceMusic.Stop();
        CancelInvoke(nameof(PlayAmbianceNote));
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
