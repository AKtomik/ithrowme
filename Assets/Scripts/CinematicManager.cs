using UnityEngine;

public class CinematicManager : MonoBehaviour
{
    [SerializeField] private BothCanvas canvasMana;
    
    private bool enabledCinematic = false;

    public void EnableCinematic()
    {
        enabledCinematic = true;
        if (SettingsStore.doCinematicStopTimer) TimerSingleton.instance.PauseTime();
        if (SettingsStore.doCinematicFreezeBodies) MovingThing.FreezeAll();
        canvasMana.StartCinematic();
    }
    
    public void DisableCinematic()
    {
        enabledCinematic = false;
        if (SettingsStore.doCinematicStopTimer) TimerSingleton.instance.PlayTime();
        if (SettingsStore.doCinematicFreezeBodies) MovingThing.UnfreezeAll();
        canvasMana.EndCinematic();
    }

    public bool IsCinematic()
    {
        return enabledCinematic;
    }
}
