using UnityEngine;

[RequireComponent(typeof(Animation))]
public class AnimationEnability : MonoBehaviour
{
    private new Animation animation;
    private bool wasPlaying;
    private float pausedTime;
    private string playingClipName;

    void Awake()
    {
        animation = GetComponent<Animation>();
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
        wasPlaying = animation.isPlaying;
        if (!wasPlaying) return;

        foreach (AnimationState state in animation)
        {
            if (animation.IsPlaying(state.name))
            {
                playingClipName = state.name;
                pausedTime = state.time;
                break;
            }
        }

        animation.Stop();
    }

    private void HandleResume()
    {
        if (!wasPlaying || string.IsNullOrEmpty(playingClipName)) return;

        animation.Play(playingClipName);
        animation[playingClipName].time = pausedTime;
    }
}