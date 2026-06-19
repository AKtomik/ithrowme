using UnityEngine;

public class CinematicManager : MonoBehaviour
{
    [SerializeField] private BothCanvas canvasMana;
    
    private bool enabledCinematic = false;
    private bool wasStopingTime = false;
    private bool wasFreezingBodies = false;
    private bool wasCanvaCine = false;

    public void EnableCinematic(bool doStopingTime = true, bool doFreezingBodies = true, bool doCanvaCine = true)
    {
        enabledCinematic = true;
        wasStopingTime = doStopingTime;
        wasFreezingBodies = doFreezingBodies;
        wasCanvaCine = doCanvaCine;
        if (SettingsStore.doCinematicStopTimer && wasStopingTime) TimerSingleton.instance.PauseTime();
        if (SettingsStore.doCinematicFreezeBodies && wasFreezingBodies) MovingThing.FreezeAll();
        if (wasCanvaCine) canvasMana.StartCinematic();
    }
    
    public void DisableCinematic()
    {
        enabledCinematic = false;
        if (SettingsStore.doCinematicStopTimer && wasStopingTime) TimerSingleton.instance.PlayTime();
        if (SettingsStore.doCinematicFreezeBodies && wasFreezingBodies) MovingThing.UnfreezeAll();
        if (wasCanvaCine) canvasMana.EndCinematic();
    }

    public bool IsCinematic()
    {
        return enabledCinematic;
    }
}
