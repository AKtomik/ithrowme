using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;

public class SettingsApply : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputActions;
    [SerializeField] private AudioMixer audioMixer;

    // called when starting or when unpaused
    public void OnEnable()
    {
        SetJoySide(SettingsStore.invertJoy);
        ApplyVolumes();
    }


    // joy side
    private void SetBindingEnabled(InputAction action, int bindingIndex, bool enabled)
    {
        //Debug.Log("action:"+action.name+" index:"+bindingIndex+" path:"+action.bindings[bindingIndex].path+" to "+enabled);
        if (enabled)
            action.RemoveBindingOverride(bindingIndex);
        else
            //action.ApplyBindingOverride(bindingIndex, new InputBinding { path = "" });
            //action.ApplyBindingOverride(bindingIndex, "");
            action.ApplyBindingOverride(bindingIndex, "<Keyboard>/f10");
    }

    public void SetJoySide(bool invert)
    {
        var lookAction = inputActions.FindAction("Player/Look");
        var rollAction = inputActions.FindAction("Player/Roll");

        if (lookAction == null || rollAction == null) return;
        Debug.Log("lookLeft:"+invert);

        // look
        SetBindingEnabled(lookAction, 1,!invert); // Left
        SetBindingEnabled(lookAction, 2, invert); // Right

        // roll
        SetBindingEnabled(rollAction, 4, invert); // Left+
        SetBindingEnabled(rollAction, 5, invert); // Left-
        SetBindingEnabled(rollAction, 7,!invert); // Right-
        SetBindingEnabled(rollAction, 8,!invert); // Right+
    }

    // audio apply
    public void ApplyVolumes()
    {
        audioMixer.SetFloat("Master", SettingsStore.masterVolume);
        audioMixer.SetFloat("SFX", SettingsStore.sfxVolume);
        audioMixer.SetFloat("Music", SettingsStore.musicVolume);
        audioMixer.SetFloat("Breath", SettingsStore.breathVolume);
        audioMixer.SetFloat("Alarm", SettingsStore.alarmVolume);
    }
}
