using UnityEngine;

abstract public class TakableLever : Takable
{
    public bool TAKE_STOP_VELOCITY = true;

    override public void Take(CapsulePlayer player)
    {
        if (TAKE_STOP_VELOCITY)
        {
            player.playerBody.linearVelocity = Vector3.zero;
            player.playerBody.angularVelocity = Vector3.zero;
        }
        OnTrigger(player);
    }
    
    override public void Throw(CapsulePlayer player) {}

    abstract public void OnTrigger(CapsulePlayer player);
}
