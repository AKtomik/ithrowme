using UnityEngine;

public class CinematicManager : MonoBehaviour
{
    [SerializeField] private UIScript canvasMana;
    
    private bool enabledCinematic = false;

    public void EnableCinematic()
    {
        enabledCinematic = true;
        if (SettingsStore.doCinematicStopTimer) TimerScript.instance.PauseTime();
        MovingThing.FreezeAll();
        canvasMana.EnableCinematicBars();
    }
    
    public void DisableCinematic()
    {
        enabledCinematic = false;
        TimerScript.instance.PlayTime();
        MovingThing.UnfreezeAll();
        canvasMana.DisableCinematicBars();
    }

    public bool IsCinematic()
    {
        return enabledCinematic;
    }
}
