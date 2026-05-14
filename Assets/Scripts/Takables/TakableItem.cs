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
    }
    
    override public void OffHand()
    {
        // enable
        collider.enabled = true;
        rigidBody.isKinematic = false;
        // reparent
        transform.SetParent(originalParentTransform);
    }
}
