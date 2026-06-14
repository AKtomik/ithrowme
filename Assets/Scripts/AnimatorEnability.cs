using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Animator))]
public class AnimatorEnability : MonoBehaviour
{
    private Animator animator;

    private struct LayerState
    {
        public int stateHash;
        public float normalizedTime;
    }
    private LayerState[] layerStates;

		void Start() => animator = GetComponent<Animator>();
    
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
        int layerCount = animator.layerCount;
        layerStates = new LayerState[layerCount];

        for (int i = 0; i < layerCount; i++)
        {
            var info = animator.GetCurrentAnimatorStateInfo(i);
            layerStates[i] = new LayerState
            {
                stateHash = info.fullPathHash,
                normalizedTime = info.normalizedTime
            };
        }
    }

    private void HandleResume()
    {
        if (layerStates == null) return;

        for (int i = 0; i < layerStates.Length; i++)
            animator.Play(layerStates[i].stateHash, i, layerStates[i].normalizedTime % 1f);

        animator.Update(0f);
    }
}