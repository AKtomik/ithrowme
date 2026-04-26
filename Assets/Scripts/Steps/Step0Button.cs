using UnityEngine;

public class Step0Button : MonoBehaviour
{
    public DoorOpeningScript doorScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other) {
        if (!other.gameObject.CompareTag("Items")) return;
        Debug.Log("door 0 opened");
        doorScript.OpeningDoors();
    }
}
