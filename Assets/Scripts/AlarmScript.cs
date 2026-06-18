using UnityEngine;

public class AlarmScript : MonoBehaviour
{
    [Header("Init")]
    [SerializeField] private bool isAlarmOn = true;
    [SerializeField] private bool isFilterOn = true;
    
    [Header("Settings")]
    [SerializeField] private Transform rotationTransform;
    [SerializeField] private float rotationSpeed = 180f;
    [SerializeField] private float cutoffFrequency = 381f;
    
    //[Header("Material")]
    //[SerializeField] private Renderer alarmRenderer;
    //[SerializeField] private Material ringingMaterial;
    //[SerializeField] private Material silentMaterial;
    
    [Header("Audio")]
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
            rotationTransform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f, Space.Self);
        }
    }

    // call
    public void PlayAlarm()
    {
        isAlarmOn = true;
        alarmAudio.Play();
    }

    public void PauseAlarm()
    {
        isAlarmOn = false;
        alarmAudio.Pause();
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
