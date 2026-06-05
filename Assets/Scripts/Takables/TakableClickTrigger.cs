using UnityEngine;

abstract public class TakableClick : Takable
{
    override public void Take(CapsulePlayer player)
    {
        if (!this.enabled) return;
        Click(player);
    }
    
    override public void Throw(CapsulePlayer player) {}

    abstract public void Click(CapsulePlayer player);

    //public void ActiveLook(CapsulePlayer player)
    //{
    //    collider.enabled = false;// disable collision during the animation
    //    isClicking = true;
    //    playerClicking = player;
    //    if (lockLooking) playerClicking.LockingLookAt(lookingPoint.position);

    //    if (takeStopVelocity)
    //    {
    //        player.playerBody.linearVelocity = Vector3.zero;
    //        player.playerBody.angularVelocity = Vector3.zero;
    //    }
    //}

    //public void FreeLook()
    //{
    //    playerClicking.UnlockingLook();
    //    playerClicking.playerBody.AddForce(-transform.forward * clickedFinishPushForce);
    //    playerClicking = null;
        
    //    if (oneTimeTrigger)
    //    {
    //        this.enabled = false;
    //    } else {
    //        collider.enabled = true;
    //    }
    //}

	//void Update()
	//{
	//	if (isClicking)
    //    {
    //        if (takeStopVelocity)
    //        {
    //            playerClicking.playerBody.linearVelocity = Vector3.zero;
    //            playerClicking.playerBody.angularVelocity = Vector3.zero;
    //        }
    //    }
	//}
}
