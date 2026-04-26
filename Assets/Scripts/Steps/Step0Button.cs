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
        if (!this.enabled && !other.gameObject.CompareTag("Items")) return;
        Debug.Log("Step0Button: door 0 opened");
        doorScript.OpeningDoors();
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z / 4);
        this.enabled = false;
    }
}
