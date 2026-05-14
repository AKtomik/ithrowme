using UnityEngine;

public class TakableLever : Takable
{
    override public void Take(CapsulePlayer player)
    {
        // stop
        player.playerBody.linearVelocity = Vector3.zero;
        player.playerBody.angularVelocity = Vector3.zero;
    }
    
    override public void Throw(CapsulePlayer player)
    {
    }
}
