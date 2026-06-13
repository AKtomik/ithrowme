using UnityEngine;
using System;

public class SoundManager : MonoBehaviour
{
	[SerializeField] private AudioSource[] alarmsAudio = new AudioSource[] {};
	
	void Start()
	{
		InitAlarm();
	}

	public void InitAlarm()
	{
    foreach (AudioSource alarm in alarmsAudio)
      alarm.gameObject.GetComponent<AudioLowPassFilter>().cutoffFrequency = 381f;
		PlayAlarm();
	}

	public void PlayAlarm()
	{
		foreach (AudioSource alarm in alarmsAudio)
			alarm.Play();
	}
	
	public void StopAlarm()
	{
		foreach (AudioSource alarm in alarmsAudio)
			alarm.Stop();
	}

	public void SetAlarmFilter(bool enabled = false)
	{
		foreach (AudioSource alarm in alarmsAudio)
			alarm.gameObject.GetComponent<AudioLowPassFilter>().enabled = enabled;
	}
}