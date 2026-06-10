
public static class SettingsStore
{
	// control settings
	public static float lookSensivity = 1;
	public static float rollSensivity = 1;
	public static bool invertRoll = true; // must be egal to the inspector value
	
	// camera settings
	public static float baseFov = 60;
	
	// hidden settings
	// all const can't be edited during play
	public const bool smartLockFinishedDoor = true;
	public const bool smartLockUnreleatedDoor = true;
	//public static bool lockBackDoor = false;
	public const bool doCinematicStopTimer = true;
	public const bool doCinematicFreezeBodies = true;
}