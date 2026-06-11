using System;
using TMPro;
using UnityEngine;

public class TimerSingleton : MonoBehaviour
{
    public static TimerSingleton instance;

    private bool ended = false;
    private bool running = false;
    private double timedSec = 0;
    [SerializeField] private TextMeshProUGUI timerTextPro;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (!running) return;
        timedSec += Time.deltaTime;
        
        if (SettingsStore.visibleTimer)
        {
            double displayMs = Math.Floor(timedSec*1000)%1000;
            string textMs = displayMs.ToString();
            textMs = new string('0', 3 - textMs.Length) + textMs;

            double displaySec = Math.Floor(timedSec);
            string textSec = displaySec.ToString();
            
            timerTextPro.text = textSec + "." + textMs;
        } else if (timerTextPro.enabled) {
            timerTextPro.enabled = false;
        }
    }
    
    // Utils methods
    public void PlayTime()
    {
        if (ended || running) return;
        Debug.Log("Timer PlayTime");
        running = true;
    }
    
    public void PauseTime()
    {
        if (!running) return;
        Debug.Log("Timer PauseTime");
        running = false;
    }
    
    public void EndTime()
    {
        Debug.Log("Timer EndTime");
        ended = true;
        running = false;
    }
}
