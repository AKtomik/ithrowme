using UnityEngine;

public class CinematicManager : MonoBehaviour
{
    [SerializeField] private UIScript canvasMana;
    
    private bool enabledCinematic = false;

    public void EnableCinematic()
    {
        enabledCinematic = true;
        if (SettingsStore.doCinematicStopTimer) TimerSingleton.instance.PauseTime();
        if (SettingsStore.doCinematicFreezeBodies) MovingThing.FreezeAll();
        canvasMana.EnableCinematicBars();
    }
    
    public void DisableCinematic()
    {
        enabledCinematic = false;
        if (SettingsStore.doCinematicStopTimer) TimerSingleton.instance.PlayTime();
        if (SettingsStore.doCinematicFreezeBodies) MovingThing.UnfreezeAll();
        canvasMana.DisableCinematicBars();
    }

    public bool IsCinematic()
    {
        return enabledCinematic;
    }
}
