using UnityEngine;

public class Step0Button : ButtonTrigger
{
    [Header("Step Pointers")]
    public DoorOpeningScript doorScript;
    [SerializeField] private Step1Lever step1Lever;

    public override void OnTrigger() {
        Debug.Log("Step0Button: door 0 opened");
        doorScript.OpeningDoors();
        step1Lever.MusicStart();
    }
}
