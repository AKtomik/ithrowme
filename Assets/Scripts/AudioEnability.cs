using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioEnability : MonoBehaviour
{
    private AudioSource source;
    private bool wasPlaying;
    private float cachedTime;

    void Awake() => source = GetComponent<AudioSource>();

    void Update()
    {
        wasPlaying = source.isPlaying;
        if (wasPlaying) cachedTime = source.time;
    }

    void OnEnable()
    {
        if (wasPlaying)
        {
            source.Play();
            source.time = cachedTime;
        }
    }
}