
public static class SettingsStore
{
	// control settings
	public static float lookSensivity = 1;
	public static float rollSensivity = 1;
	public static float musicVolume = 0;
    public static float sfxVolume = 0;
    public static float masterVolume = 0;
	public static float breathVolume = 0;
    public static float alarmVolume = 0;
    public static bool invertRoll = false; // must be egal to the inspector value
	
	// camera settings
	public static float baseFov = 60;
	
	// compfort settings
	public static bool visibleTimer = true;
	
	// hidden settings
	// all const can't be edited during play
	public const bool smartLockFinishedDoor = true;
	public const bool smartLockUnreleatedDoor = true;
	//public static bool lockBackDoor = false;
	public const bool doCinematicStopTimer = true;
	public const bool doCinematicFreezeBodies = true;


}