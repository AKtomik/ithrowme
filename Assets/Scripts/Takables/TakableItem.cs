using UnityEngine;

public class TakableItem : Takable
{
    [SerializeField] protected Rigidbody rigidBody;
    private Transform originalParentTransform;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Start()
    {
        originalParentTransform = transform.parent;
        base.Start();
    }

    override public void InHand(CapsulePlayer player)
    {
        // stop
        rigidBody.linearVelocity = Vector3.zero;
        rigidBody.angularVelocity = Vector3.zero;
        // disable
        collider.enabled = false;
        rigidBody.isKinematic = true;
        // reparent
        Transform handTransform = player.handPoint;
        transform.SetParent(handTransform);
        transform.position = handTransform.position;
        // reset pos
        if (collider.gameObject != gameObject)
            collider.gameObject.transform.position = handTransform.position;
        // put in hand
        player.PutInHand(this);
    }
    
    override public void OffHand(CapsulePlayer player)
    {
        // enable
        collider.enabled = true;
        rigidBody.isKinematic = false;
        // reparent
        transform.SetParent(originalParentTransform);
        // move the projectile
        transform.SetPositionAndRotation(player.throwPoint.position, player.throwPoint.rotation);
        
        // throw
        float throwCommonForce = player.throwMassBase + rigidBody.mass * player.throwMassInfluence;
        // throw the projectile
        rigidBody.AddForce(throwCommonForce * player.throwObjectForce * player.throwPoint.forward, ForceMode.Impulse);
        // throw the player
        player.playerBody.AddForce(throwCommonForce * player.throwPlayerForce * transform.forward, ForceMode.Impulse);

        // remove from hand
        player.ClearHand();
    }
}
