
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float shakeAmount = 0.02f;
    public bool toShake = false;
    private Vector3 initalPos;

    private void Awake()
    {
        initalPos = transform.position;
    }



    // Update is called once per frame
    void Update()
    {
        if (toShake)
        {
            transform.position = initalPos + Random.insideUnitSphere * shakeAmount;
        }
            
    }
}
