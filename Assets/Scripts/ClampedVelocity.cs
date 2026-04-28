using UnityEngine;

public class ClampedVelocity : MonoBehaviour
{
    public bool clampMin = true;
    public float speedMin = 5f;
    public bool clampMax = false;
    public float speedMax = 10f;

    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (rb.linearVelocity == Vector3.zero) return;// do nothing when no direction
        
        float speed = rb.linearVelocity.magnitude;
        if (clampMin && speed < speedMin)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * speedMin;
        }
        else if (clampMax && speed > speedMax)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * speedMax;
        }
    }
}