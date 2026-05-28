using UnityEngine;

public class Step0Button : ButtonTrigger
{
    public DoorOpeningScript doorScript;

    public override void OnTrigger() {
        Debug.Log("Step0Button: door 0 opened");
        doorScript.OpeningDoors();
    }
}
