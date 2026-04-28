using System;
using System.Timers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    public static TimerScript instance;

    public bool running = false;
    private double timedSec = 0;
    private TextMeshProUGUI textPro;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
        textPro = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!running) return;
        timedSec += Time.deltaTime;

        double displayMs = Math.Floor(timedSec*1000)%1000;
        string textMs = displayMs.ToString();

        double displaySec = Math.Floor(timedSec);
        string textSec = displaySec.ToString();
        
        textPro.text = textSec + "." + textMs;
    }
}
