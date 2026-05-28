using UnityEngine;

abstract public class TakableLever : Takable
{
    public bool TAKE_STOP_VELOCITY = true;
    
    public bool ONE_TIME_TRIGGER = true;
    public Vector3 TRIGGER_SCALE = new Vector3(1, -1, .25f);

    override public void Take(CapsulePlayer player)
    {
        if (!this.enabled) return;

        if (TAKE_STOP_VELOCITY)
        {
            player.playerBody.linearVelocity = Vector3.zero;
            player.playerBody.angularVelocity = Vector3.zero;
        }
        
        OnTrigger(player);
        
        transform.localScale = new Vector3(transform.localScale.x * TRIGGER_SCALE.x, transform.localScale.y * TRIGGER_SCALE.y, transform.localScale.z * TRIGGER_SCALE.z);
        if (ONE_TIME_TRIGGER)
        {
            this.enabled = false;
        }
    }
    
    override public void Throw(CapsulePlayer player) {}

    abstract public void OnTrigger(CapsulePlayer player);
}
