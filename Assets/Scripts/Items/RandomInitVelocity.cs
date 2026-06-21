using UnityEngine;

public class RandomInitVelocity : MonoBehaviour
{
    [SerializeField] bool randomRotationPos = true;
    [SerializeField] bool randomRotationVelocity = true;
    [SerializeField, DrawIf("randomRotationVelocity")] float randomRotationVelocityMin = 0;
    [SerializeField, DrawIf("randomRotationVelocity")] float randomRotationVelocityMax = .5f;

    [SerializeField] bool initalPush = false;
    [SerializeField, DrawIf("initalPush")] Vector3 initalPushForce = new Vector3(.2f, 0f, 0f);

    private Rigidbody rb;

    Vector3 GetRandomRotation()
    {
        return new Vector3(Random.value * 360, Random.value * 360, Random.value * 360);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (randomRotationPos)
            transform.rotation = Quaternion.Euler(GetRandomRotation());
        if (randomRotationVelocity)
            rb.AddTorque(GetRandomRotation() * Random.Range(randomRotationVelocityMin, randomRotationVelocityMax));
        if (initalPush)
            rb.AddForce(initalPushForce);
    }
}
