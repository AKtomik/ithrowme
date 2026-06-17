using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioEnability : MonoBehaviour
{
    private AudioSource source;
    private bool wasPlaying;
    private float pausedTime;

	void Start() => source = GetComponent<AudioSource>();

	void OnEnable()
    {
        PauseSignal.OnPause += HandlePause;
        PauseSignal.OnResume += HandleResume;
    }

    void OnDisable()
    {
        PauseSignal.OnPause -= HandlePause;
        PauseSignal.OnResume -= HandleResume;
    }

    private void HandlePause()
    {
        wasPlaying = source.isPlaying;
        if (wasPlaying)
        {
            pausedTime = source.time;
            source.Pause();
        }
    }

    private void HandleResume()
    {
        if (wasPlaying)
        {
            source.Play();
            source.time = pausedTime;
        }
    }
}