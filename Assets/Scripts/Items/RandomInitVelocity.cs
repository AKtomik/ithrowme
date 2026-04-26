using UnityEngine;

public class RandomInitVelocity : MonoBehaviour
{
    [SerializeField] bool randomRotationPos = true;
    [SerializeField] bool randomRotationVelocity = true;
    [SerializeField] float randomRotationVelocityMin = 0;
    [SerializeField] float randomRotationVelocityMax = .5f;

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

    }
}
