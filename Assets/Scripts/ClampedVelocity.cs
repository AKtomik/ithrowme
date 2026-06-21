using UnityEngine;

public class ClampedVelocity : MonoBehaviour
{
    public bool clampMin = true;
    [DrawIf("clampMin")] public float speedMin = 1f;
    
    public bool clampMax = false;
    [DrawIf("clampMax")] public float speedMax = 10f;

    public bool stopBellowNoConsideration = true;
    [DrawIf("stopBellowNoConsideration")] public float noConsiderationSpeed = .000001f;

    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (rb.linearVelocity == Vector3.zero) return;// do nothing when no direction
        
        float speed = rb.linearVelocity.magnitude;
        if (stopBellowNoConsideration && speed <= noConsiderationSpeed) return;

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