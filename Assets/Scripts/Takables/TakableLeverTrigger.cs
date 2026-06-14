using UnityEngine;

abstract public class TakableLever : Takable
{
    [Header("Trigger Pull")]
    public bool oneTimeTrigger = true;
    public bool lockLooking = true;
    public bool cinematicMode = true;
        
    [Header("Trigger Move")]
    public bool takeStopVelocity = true;
    public float pulledFinishPushForce = 10f;

    [Header("Trigger Pointers")]
    [SerializeField] private Transform lookingPoint;
    [SerializeField] private Animation pulledAnimatorReference;
    [SerializeField] private string pulledAnimationName;
    [SerializeField] private GameObject[] activatedObjects;

    private bool pulling;
    private CapsulePlayer playerPulling;

	override public void Take(CapsulePlayer player)
    {
        
        if (!this.enabled) return;
        
        if (takeCollider) takeCollider.enabled = false;// disable collision during the animation
        pulling = true;
        playerPulling = player;

        if (takeStopVelocity)
        {// need to do before player is kinematic in cinematic
            player.playerBody.linearVelocity = Vector3.zero;
            player.playerBody.angularVelocity = Vector3.zero;
        }
        
        if (cinematicMode) ReferenceSingleton.instance.cinematicManager.EnableCinematic();
        if (lockLooking) playerPulling.LockingLookAt(lookingPoint.position);
        
        if (pulledAnimationName.Length > 0)
            pulledAnimatorReference.Play(pulledAnimationName);
        else
            pulledAnimatorReference.Play();
        
        PullStart(player);
        
    }
        
    override public void Throw(CapsulePlayer player) {}

    abstract public void PullStart(CapsulePlayer player);
    abstract public void PullFinish(CapsulePlayer player);

    public bool IsPulling() => pulling;

    public void AnimationEnded()
    {
        if (cinematicMode) ReferenceSingleton.instance.cinematicManager.DisableCinematic();
        if (lockLooking) playerPulling.UnlockingLook();
        if (pulledFinishPushForce != 0) playerPulling.playerBody.AddForce(-transform.forward * pulledFinishPushForce);
        foreach (var item in activatedObjects)
            item.SetActive(true);
        
        PullFinish(playerPulling);
        pulling = false;
        playerPulling = null;
        
        if (oneTimeTrigger)
        {
            this.enabled = false;
        } else {
            if (takeCollider) takeCollider.enabled = true;
        }
    }

    public void AnimationHandState(HandState state)
    {
        ReferenceSingleton.instance.bothCanvas.ChangeCinematicHandState(state);
    }
    
    public void AnimationLookingPoint(Transform newLookingPoint, float speed = 1)
    {// ! does not works in animationPlayer, need to do aliases
        if (lockLooking) playerPulling.LockingLookAt(newLookingPoint.position, speed);
    }

    public void PlayAudio()
    {
        audioSource.Play();
    }
}
