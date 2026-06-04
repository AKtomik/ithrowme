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

    public void Put(Transform parent)
    {
        // stop
        rigidBody.linearVelocity = Vector3.zero;
        rigidBody.angularVelocity = Vector3.zero;
        // disable
        collider.enabled = false;
        rigidBody.isKinematic = true;
        // reparent
        transform.SetParent(parent);
        transform.position = parent.position;
        // reset pos
        if (collider.gameObject != gameObject)
            collider.gameObject.transform.position = parent.position;
    }
    
    public void Unput(Transform point)
    {
        // enable
        collider.enabled = true;
        rigidBody.isKinematic = false;
        // reparent
        transform.SetParent(originalParentTransform);
        // move the projectile
        transform.SetPositionAndRotation(point.position, point.rotation);
    }

    override public void Take(CapsulePlayer player)
    {
        Put(player.handPoint);
        player.PutInHand(this);
    }
    
    override public void Throw(CapsulePlayer player)
    {
        Unput(player.throwPoint);

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
