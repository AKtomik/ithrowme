using UnityEngine;

public class AlarmScript : MonoBehaviour
{
    [SerializeField] private bool isAlarmOn = true;
    [SerializeField] private bool isFilterOn = true;
    [SerializeField] private float rotationSpeed = 180f;
    [SerializeField] private float cutoffFrequency = 381f;
    [SerializeField] private AudioSource alarmAudio;
    [SerializeField] private AudioLowPassFilter alarmLowpassFilter;

    // loop
    public void Start()
    {
        alarmLowpassFilter.cutoffFrequency = cutoffFrequency;
        
        EnableFilter(isFilterOn);
        if (isAlarmOn) PlayAlarm();
        else PauseAlarm();
        //Refresh();
    }

    private void Update()
    {
        if (isAlarmOn)
        {
            transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f, Space.Self);
        }
    }

    // refresh
    public void Refresh()
    {// own refresh function
        alarmAudio.playOnAwake = isAlarmOn;
    }

    public void OnEnable()
    {
        Refresh();
    }

    // call
    public void PlayAlarm()
    {
        isAlarmOn = true;
        alarmAudio.Play();
        Refresh();
    }

    public void PauseAlarm()
    {
        isAlarmOn = false;
        alarmAudio.Pause();
        Refresh();
    }
        
    public void EnableFilter(bool enabled = false)
    {
        isFilterOn = enabled;
        alarmLowpassFilter.enabled = enabled;
    }

    public void KillAlarm()
    {
        Destroy(gameObject);
    }
}
