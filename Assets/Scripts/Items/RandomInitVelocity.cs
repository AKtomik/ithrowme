using UnityEngine;

public class RandomInitVelocity : MonoBehaviour
{


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.rotation = Quaternion.Euler(new Vector3(Random.value * 360, Random.value * 360, Random.value * 360));
    }
}
