using System;

public static class PauseSignal
{
    public static event Action OnPause;
    public static event Action OnResume;

    public static void RaisePause() => OnPause?.Invoke();
    public static void RaiseResume() => OnResume?.Invoke();
}