using UnityEngine;

public class TakableObject : MonoBehaviour
{
    [SerializeField] private new Collider collider;
    [SerializeField] private Rigidbody rigidBody;
    private Transform originalParentTransform;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalParentTransform = transform.parent;
        TakableReference takableReference = collider.gameObject.AddComponent(typeof(TakableReference)) as TakableReference;
        takableReference.takableObject = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InHand(Transform handTransform)
    {
        // stop
        rigidBody.linearVelocity = Vector3.zero;
        rigidBody.angularVelocity = Vector3.zero;
        // disable
        collider.enabled = false;
        rigidBody.isKinematic = true;
        // reparent
        transform.SetParent(handTransform);
        transform.position = handTransform.position;
    }
    
    public void OffHand()
    {
        // enable
        collider.enabled = true;
        rigidBody.isKinematic = false;
        // reparent
        transform.SetParent(originalParentTransform);
    }
}
