using UnityEngine;

abstract public class TakableLever : Takable
{
    public bool takeStopVelocity = true;
    
    public bool oneTimeTrigger = true;
    public bool lockLooking = true;

    [SerializeField] private Transform lookingPoint;
    [SerializeField] private Animation pulledAnimatorReference;
    public float pulledFinishPushForce = 10f;

    private bool isPulling;
    private CapsulePlayer playerPulling;

    override public void Take(CapsulePlayer player)
    {
        if (!this.enabled) return;

        collider.enabled = false;// disable collision during the animation
        isPulling = true;
        playerPulling = player;
        if (lockLooking) playerPulling.LockingLookAt(lookingPoint.position);

        if (takeStopVelocity)
        {
            player.playerBody.linearVelocity = Vector3.zero;
            player.playerBody.angularVelocity = Vector3.zero;
        }
        
        pulledAnimatorReference.Play();
        PullStart(player);
    }
    
    override public void Throw(CapsulePlayer player) {}

    abstract public void PullStart(CapsulePlayer player);
    abstract public void PullFinish(CapsulePlayer player);

    public void AnimationEnded()
    {
        playerPulling.UnlockingLook();
        playerPulling.playerBody.AddForce(-transform.forward * pulledFinishPushForce);
        
        if (lockLooking) PullFinish(playerPulling);
        playerPulling = null;
        
        if (oneTimeTrigger)
        {
            this.enabled = false;
        } else {
            collider.enabled = true;
        }
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
