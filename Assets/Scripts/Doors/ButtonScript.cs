using UnityEngine;

public class ButtonScript : MonoBehaviour
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

    private void OnTriggerEnter(Collider other)
    {
        if (doorScript)
        {
            if (other.CompareTag("Items"))
            {
                doorScript.OpeningDoors();
                
                //// détruire item
            }
        }

    }
}
