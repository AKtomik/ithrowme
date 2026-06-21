using System.Globalization;
using UnityEngine;



public static class SettingsStore
{
    public static double personalBest = 0.0;
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

    

    public static void SaveSettings()
    {
        PlayerPrefs.SetFloat("lookSensivity", SettingsStore.lookSensivity);
        PlayerPrefs.SetFloat("rollSensivity", SettingsStore.rollSensivity);
        PlayerPrefs.SetFloat("musicVolume", SettingsStore.musicVolume);
        PlayerPrefs.SetFloat("sfxVolume", SettingsStore.sfxVolume);
        PlayerPrefs.SetFloat("masterVolume", SettingsStore.masterVolume);
        PlayerPrefs.SetFloat("breathVolume", SettingsStore.breathVolume);
        PlayerPrefs.SetFloat("alarmVolume", SettingsStore.alarmVolume);
        PlayerPrefs.SetInt("invertRoll", SettingsStore.invertRoll ? 1 : 0);
        PlayerPrefs.SetFloat("baseFov", SettingsStore.baseFov);
        PlayerPrefs.SetInt("visibleTimer", SettingsStore.visibleTimer ? 1 : 0);
        PlayerPrefs.SetString("personalBest", SettingsStore.personalBest.ToString(CultureInfo.InvariantCulture));
        PlayerPrefs.Save();
    }

    public static void LoadSettings()
    {
        SettingsStore.lookSensivity = PlayerPrefs.GetFloat("lookSensivity", 1);
        SettingsStore.rollSensivity = PlayerPrefs.GetFloat("rollSensivity", 1);
        SettingsStore.musicVolume = PlayerPrefs.GetFloat("musicVolume", 0);
        SettingsStore.sfxVolume = PlayerPrefs.GetFloat("sfxVolume", 0);
        SettingsStore.masterVolume = PlayerPrefs.GetFloat("masterVolume", 0);
        SettingsStore.breathVolume = PlayerPrefs.GetFloat("breathVolume", 0);
        SettingsStore.alarmVolume = PlayerPrefs.GetFloat("alarmVolume", 0);
        SettingsStore.invertRoll = PlayerPrefs.GetInt("invertRoll", 0) == 1;
        SettingsStore.baseFov = PlayerPrefs.GetFloat("baseFov", 60);
        SettingsStore.visibleTimer = PlayerPrefs.GetInt("visibleTimer", 1) == 1;
        string raw = PlayerPrefs.GetString("personalBest", "0.0");
        if (!double.TryParse(raw, NumberStyles.Float, CultureInfo.InvariantCulture, out SettingsStore.personalBest))
            SettingsStore.personalBest = 0.0;

    }

    public static void ResetToDefaults()
    {
        lookSensivity = 1f;
        rollSensivity = 1f;
        musicVolume = 0f;
        sfxVolume = 0f;
        masterVolume = 0f;
        breathVolume = 0f;
        alarmVolume = 0f;
        invertRoll = false;
        baseFov = 60f;
        visibleTimer = true;
        SaveSettings();
    }

}
