using UnityEngine;

public class CinematicManager : MonoBehaviour
{
    [SerializeField] private UIScript canvasMana;

    public void EnableCinematic()
    {
        if (SettingsStore.doCinematicStopTimer) TimerScript.instance.PauseTime();
        canvasMana.EnableCinematicBars();
    }
    
    public void DisableCinematic()
    {
        TimerScript.instance.PlayTime();
        canvasMana.DisableCinematicBars();
    }
}
