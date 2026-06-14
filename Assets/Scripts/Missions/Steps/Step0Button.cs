using UnityEngine;

public class Step0Button : ButtonTrigger
{
    [Header("Step Pointers")]
    [SerializeField] private DoorOpeningScript doorScript;
    [SerializeField] private AudioManager audioManager;

    public override void OnTrigger() {
        Debug.Log("Step0Button: door 0 opened");
        doorScript.OpeningDoors();
        audioManager.PlayRunMusic();
        audioManager.SetAlarmsFilter(false);
    }
}
