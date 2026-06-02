using UnityEngine;
using UnityEngine.Audio;

abstract public class TakableLever : Takable
{
    public bool TAKE_STOP_VELOCITY = true;
    
    public bool ONE_TIME_TRIGGER = true;

    [SerializeField] private Animation pulledAnimatorReference;
    public float pulledFinishPushForce = 10f;

    private bool isPulling;
    private CapsulePlayer playerPulling;

    [Header("Audio")]
    [SerializeField] private AudioClip leverAction;




    override public void Take(CapsulePlayer player)
    {
        
        if (!this.enabled) return;

        collider.enabled = false;// disable collision during the animation
        isPulling = true;
        playerPulling = player;

        if (TAKE_STOP_VELOCITY)
        {
            player.playerBody.linearVelocity = Vector3.zero;
            player.playerBody.angularVelocity = Vector3.zero;
        }
        
        pulledAnimatorReference.Play();
        PullStart(player);
        audioSource.PlayOneShot(leverAction);
    }
    
    override public void Throw(CapsulePlayer player) {}

    abstract public void PullStart(CapsulePlayer player);

    virtual public void PullFinish()
    {
        playerPulling.playerBody.AddForce(transform.forward * pulledFinishPushForce);
        playerPulling = null;
        
        if (ONE_TIME_TRIGGER)
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
            if (TAKE_STOP_VELOCITY)
            {
                playerPulling.playerBody.linearVelocity = Vector3.zero;
                playerPulling.playerBody.angularVelocity = Vector3.zero;
            }
        }
	}
}
