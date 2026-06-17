using UnityEngine;
using System.Collections.Generic;
using System;

[RequireComponent(typeof(Animator)), Obsolete]
// does not works correctly!
// is not planeded to be patched, don't rely on it
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