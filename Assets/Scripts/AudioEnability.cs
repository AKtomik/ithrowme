using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioEnability : MonoBehaviour
{
    private AudioSource source;
    private bool wasPlaying;
    private float pausedTime;

	void Start() => source = GetComponent<AudioSource>();

	void Awake()
    {
        PauseSignal.OnPause += HandlePause;
        PauseSignal.OnResume += HandleResume;
    }

    void OnDestroy()
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