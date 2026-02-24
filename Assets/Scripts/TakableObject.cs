using UnityEngine;

public class TakableObject : MonoBehaviour
{
    [SerializeField] private new Collider collider;
    private Rigidbody rb;
    private Transform originalParentTransform;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        originalParentTransform = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InHand(Transform handTransform)
    {
        // stop
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        // disable
        collider.enabled = false;
        rb.isKinematic = true;
        // reparent
        transform.SetParent(handTransform);
        transform.position = handTransform.position;
    }
    
    public void OffHand()
    {
        // enable
        collider.enabled = true;
        Debug.Log("hello off hand owo");
        rb.isKinematic = false;
        // reparent
        transform.SetParent(originalParentTransform);
    }
}
