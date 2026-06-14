using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;
using static UnityEngine.Timeline.DirectorControlPlayable;

public class PauseMenuScript : MonoBehaviour
{

    private PauseManager pauseManager;
    private GameObject pauseManagerGO;

    [SerializeField] private InputActionAsset inputActions;
    
    [Header("Canvas")]
    
    [SerializeField] private GameObject mainCanva;
    [SerializeField] private GameObject settingsCanva;
    [SerializeField] private GameObject audioCanva;
    [SerializeField] private GameObject quitCanva;
    [SerializeField] private GameObject gamepadCanva;
    [SerializeField] private GameObject keyboardCanva;

    [Header("Settings parameters")]
    [SerializeField] private Slider sliderLookSensitivity;
    [SerializeField] private Slider sliderRollSensitivity;
    [SerializeField] private Slider sliderSfxVolume;
    [SerializeField] private Slider sliderMainVolume;
    [SerializeField] private Slider sliderMusicVolume;
    [SerializeField] private Slider sliderBreathVolume;
    [SerializeField] private Slider sliderAlarmVolume;

    [SerializeField] private Toggle toggleRollAxis;
    [SerializeField] private Toggle toggleRollJoystick;


    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioSource changeButtonAudio;
    
    [SerializeField] private AudioSource sfxSlidersAudio;

    [Header("First Selected Options")]
    [SerializeField] private GameObject mainMenuFirst;
    [SerializeField] private GameObject settingsMenuFirst;
    [SerializeField] private GameObject quitMenuFirst;
    [SerializeField] private GameObject gamepadFirst;
    [SerializeField] private GameObject keyboardFirst;
    [SerializeField] private GameObject audioFirst;

    private InputAction navigateActions;
    private GameObject previousSelectedGameobject;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
        pauseManagerGO = GameObject.FindGameObjectWithTag("PauseManager");
        if (pauseManagerGO != null )
        {
            pauseManager = pauseManagerGO.GetComponent<PauseManager>();
        }
        

    }
    private void OnEnable()
    {
        GoToMainCanva();
        previousSelectedGameobject = EventSystem.current.currentSelectedGameObject;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (previousSelectedGameobject != EventSystem.current.currentSelectedGameObject)
        {
            ButtonsSound();
            previousSelectedGameobject = EventSystem.current.currentSelectedGameObject;
        }
        */

        

    }

    ///////////////////////////////////////////////////////////////
    /////////////////////* Change Canva FUNCTIONS*/////////////////
    ///////////////////////////////////////////////////////////////

    public void GoToMainCanva()
    {
        mainCanva.SetActive(true);
        settingsCanva.SetActive(false);
        quitCanva.SetActive(false);
        gamepadCanva.SetActive(false);
        keyboardCanva.SetActive(false);
        audioCanva.SetActive(false);

        EventSystem.current.SetSelectedGameObject(mainMenuFirst);
    }

    public void ClickOnQuitButton()
    {
        mainCanva.SetActive(false);
        quitCanva.SetActive(true);
        EventSystem.current.SetSelectedGameObject(quitMenuFirst);
    }


    public void GoToSettings()
    {
        mainCanva.SetActive(false);
        gamepadCanva.SetActive(false);
        keyboardCanva.SetActive(false);
        audioCanva.SetActive(false);

        settingsCanva.SetActive(true);

        sliderLookSensitivity.value = SettingsStore.lookSensivity;
        sliderRollSensitivity.value = SettingsStore.rollSensivity;

        toggleRollAxis.isOn = SettingsStore.invertRoll;

        EventSystem.current.SetSelectedGameObject(settingsMenuFirst);
    }

    public void GoToAudio()
    {
        settingsCanva.SetActive(false);
        audioCanva.SetActive(true);

        sliderMusicVolume.value = SettingsStore.musicVolume;
        sliderSfxVolume.value = SettingsStore.sfxVolume;
        sliderMainVolume.value = SettingsStore.masterVolume;
        sliderBreathVolume.value = SettingsStore.breathVolume;
        sliderAlarmVolume.value = SettingsStore.alarmVolume;

        audioMixer.GetFloat("SFX", out float sfxVolume);
        sliderSfxVolume.value = sfxVolume;

        audioMixer.GetFloat("Master", out float masterVolume);
        sliderMainVolume.value = masterVolume;

        audioMixer.GetFloat("Music", out float musicVolume);
        sliderMainVolume.value = musicVolume;

        audioMixer.GetFloat("Breath", out float breathVolume);
        sliderBreathVolume.value = breathVolume;

        audioMixer.GetFloat("Alarm", out float alarmVolume);
        sliderAlarmVolume.value = alarmVolume;

        EventSystem.current.SetSelectedGameObject(audioFirst);
    }

    public void GoToGamepad()
    {
        settingsCanva.SetActive(false);
        gamepadCanva.SetActive(true);
        EventSystem.current.SetSelectedGameObject(gamepadFirst);
        //SelectFirstButton();
    }

    public void GoToKeyboard()
    {
        settingsCanva.SetActive(false);
        keyboardCanva.SetActive(true);
        EventSystem.current.SetSelectedGameObject(keyboardFirst);
        //SelectFirstButton();
    }

    ///////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////



    public void Resume()
    {
        pauseManager.Resume(); // If failed : PauseManager must have the tag "PauseManager"
       
    }


    public void QuitGame()
    {
        Debug.Log("Game is quitting...");
        Application.Quit();
    }

    private bool IsRightStickComposite(InputAction action, int compositeIndex)
    {
        for (int j = compositeIndex + 1; j < action.bindings.Count; j++)
        {
            if (!action.bindings[j].isPartOfComposite) break;

            if (action.bindings[j].effectivePath.Contains("rightStick"))
                return true;
        }
        return false;
    }

    public void ToggleRollInput(bool useR1L1)
    {
        var action = inputActions.FindActionMap("Player")?.FindAction("Roll");
        if (action == null) return;

        bool insideTargetComposite = false;

        for (int i = 0; i < action.bindings.Count; i++)
        {
            var binding = action.bindings[i];

            if (binding.isComposite)
            {
                insideTargetComposite = IsRightStickComposite(action, i);
                continue;
            }

            if (!insideTargetComposite || !binding.isPartOfComposite) continue;

            if (useR1L1)
            {
                if (binding.name == "negative")
                    action.ApplyBindingOverride(i, "<Gamepad>/leftShoulder");   // R1
                else if (binding.name == "positive")
                    action.ApplyBindingOverride(i, "<Gamepad>/rightShoulder"); // R1
            }
            else
            {
                action.RemoveBindingOverride(i); // Restore rightStick
            }
        }
    }

    ///////////////////////////////////////////////////////////////
    /////////////////////* SETTINGS FUNCTIONS*/////////////////////
    ///////////////////////////////////////////////////////////////

    public void ChangeLookSensitivity()
    {
        sfxSlidersAudio.Play();
        SettingsStore.lookSensivity = sliderLookSensitivity.value;
    }
    

    public void ChangeRollSensitivity()
    {
        sfxSlidersAudio.Play();
        SettingsStore.rollSensivity = sliderRollSensitivity.value;
    }

    public void ChangeSFXVolume()
    {
        sfxSlidersAudio.Play();
        audioMixer.SetFloat("SFX", sliderSfxVolume.value);
        SettingsStore.sfxVolume = sliderSfxVolume.value;
    }
    public void ChangeMainVolume()
    {
        sfxSlidersAudio.Play();
        audioMixer.SetFloat("Master", sliderMainVolume.value);
        SettingsStore.masterVolume = sliderMainVolume.value;
    }
    public void ChangeMusicVolume()
    {
        sfxSlidersAudio.Play();
        audioMixer.SetFloat("Music", sliderMusicVolume.value);
        SettingsStore.musicVolume = sliderMusicVolume.value;
    }

    public void ChangeBreathVolume()
    {
        sfxSlidersAudio.Play();
        audioMixer.SetFloat("Breath", sliderBreathVolume.value);
        SettingsStore.breathVolume = sliderBreathVolume.value;
    }

    public void ChangeAlarmVolume()
    {
        sfxSlidersAudio.Play();
        audioMixer.SetFloat("Alarm", sliderAlarmVolume.value);
        SettingsStore.alarmVolume = sliderAlarmVolume.value;
    }

    public void InvertRollAxis()
    {
        sfxSlidersAudio.Play();
        SettingsStore.invertRoll = toggleRollAxis.isOn;
    }

    public void UseR1L1()
    {
        ToggleRollInput(toggleRollJoystick.isOn);
    }

    ///////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////


    
    public void ButtonsSound()
    {
        Debug.Log("Button changed");
        changeButtonAudio.PlayOneShot(changeButtonAudio.clip);
    }



}
