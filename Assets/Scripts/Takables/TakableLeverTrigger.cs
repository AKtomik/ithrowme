using UnityEngine;
using UnityEngine.Audio;

abstract public class TakableLever : Takable
{
    [Header("Trigger Pull")]
    public bool oneTimeTrigger = true;
    public bool lockLooking = true;
    
    [Header("Trigger Move")]
    public bool takeStopVelocity = true;
    public float pulledFinishPushForce = 10f;

    [Header("Trigger Pointers")]
    [SerializeField] private Transform lookingPoint;
    [SerializeField] private Animation pulledAnimatorReference;
    [SerializeField] private string pulledAnimationName;

    private bool isPulling;
    private CapsulePlayer playerPulling;

    [Header("Audio")]
    [SerializeField] private AudioClip leverAction;




    override public void Take(CapsulePlayer player)
    {
        
        if (!this.enabled) return;

        if (takeCollider) takeCollider.enabled = false;// disable collision during the animation
        isPulling = true;
        playerPulling = player;
        if (lockLooking) playerPulling.LockingLookAt(lookingPoint.position);

        if (takeStopVelocity)
        {
            player.playerBody.linearVelocity = Vector3.zero;
            player.playerBody.angularVelocity = Vector3.zero;
        }
        
        if (pulledAnimationName.Length > 0)
            pulledAnimatorReference.Play(pulledAnimationName);
        else
            pulledAnimatorReference.Play();
        PullStart(player);
        
    }
    
    override public void Throw(CapsulePlayer player) {}

    abstract public void PullStart(CapsulePlayer player);
    abstract public void PullFinish(CapsulePlayer player);

    public void AnimationEnded()
    {
        if (lockLooking) playerPulling.UnlockingLook();
        if (pulledFinishPushForce != 0) playerPulling.playerBody.AddForce(-transform.forward * pulledFinishPushForce);
        
        PullFinish(playerPulling);
        playerPulling = null;
        
        if (oneTimeTrigger)
        {
            this.enabled = false;
        } else {
            if (takeCollider) takeCollider.enabled = true;
        }
    }


    public void PlayAudio()
    {
        audioSource.PlayOneShot(leverAction);
    }

	void Update()
	{
		if (isPulling)
        {
            if (takeStopVelocity)
            {
                playerPulling.playerBody.linearVelocity = Vector3.zero;
                playerPulling.playerBody.angularVelocity = Vector3.zero;
            }
        }
	}
}
