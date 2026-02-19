using UnityEngine;

public class TakableObject : MonoBehaviour
{
    [SerializeField] private new Collider collider;
    private Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InHand()
    {
        collider.enabled = false;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;
    }
    
    public void OffHand()
    {
        collider.enabled = true;
        rb.isKinematic = false;
    }
}
