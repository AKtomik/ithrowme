using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
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

    private void OnCollisionEnter(Collision collision)
    {
        if (doorScript)
        {
            if (collision.gameObject.CompareTag("Items"))
            {
                doorScript.OpeningDoors();

                //// détruire item
            }
        }
    }



}
