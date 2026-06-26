using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;
using static UnityEngine.Timeline.DirectorControlPlayable;

public class PauseMenuScript : MonoBehaviour
{

    private PauseManager pauseManager;
    private GameObject pauseManagerGO;

    
    [Header("Canvas")]
    
    [SerializeField] private GameObject mainCanva;
    [SerializeField] private GameObject settingsCanva;
    [SerializeField] private GameObject audioCanva;
    [SerializeField] private GameObject quitCanva;
    [SerializeField] private GameObject gamepadCanva;
    [SerializeField] private GameObject keyboardCanva;
    [SerializeField] private GameObject menuCanva;

    [Header("Settings parameters")]
    [SerializeField] private TextMeshProUGUI FOVText;
    [SerializeField] private UnityEngine.UI.Slider sliderLookSensitivity;
    [SerializeField] private UnityEngine.UI.Slider sliderRollSensitivity;

    [SerializeField] private UnityEngine.UI.Slider sliderFOV;

    [SerializeField] private UnityEngine.UI.Slider sliderSfxVolume;
    [SerializeField] private UnityEngine.UI.Slider sliderMainVolume;
    [SerializeField] private UnityEngine.UI.Slider sliderMusicVolume;
    [SerializeField] private UnityEngine.UI.Slider sliderBreathVolume;
    [SerializeField] private UnityEngine.UI.Slider sliderAlarmVolume;


    [SerializeField] private UnityEngine.UI.Toggle toggleRollAxis;
    [SerializeField] private UnityEngine.UI.Toggle toggleUseVsync;
    [SerializeField] private UnityEngine.UI.Toggle toggleDisableOutline;
    [SerializeField] private UnityEngine.UI.Toggle toggleInvertTouch;

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
    [SerializeField] private GameObject menuFirst;

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
        SettingsStore.LoadSettings();
        GoToMainCanva();
        previousSelectedGameobject = EventSystem.current.currentSelectedGameObject;
    }


    private void OnDisable()
    {
        SettingsStore.SaveSettings();
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

    private void OnApplicationQuit()
    {
        SettingsStore.SaveSettings();
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
        sliderFOV.value = SettingsStore.baseFov;
        sliderFOV.value = SettingsStore.baseFov;
        FOVText.text = Mathf.Round(sliderFOV.value) + "";
        toggleRollAxis.isOn = SettingsStore.invertRoll;

        toggleUseVsync.isOn = SettingsStore.useVSync;
        toggleDisableOutline.isOn = SettingsStore.disableOutline;

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
        ToPercent(sliderSfxVolume);

        audioMixer.GetFloat("Master", out float masterVolume);
        sliderMainVolume.value = masterVolume;
        ToPercent(sliderMainVolume);

        audioMixer.GetFloat("Music", out float musicVolume);
        sliderMusicVolume.value = musicVolume;
        ToPercent(sliderMusicVolume);

        audioMixer.GetFloat("Breath", out float breathVolume);
        sliderBreathVolume.value = breathVolume;
        ToPercent(sliderBreathVolume);

        audioMixer.GetFloat("Alarm", out float alarmVolume);
        sliderAlarmVolume.value = alarmVolume;
        ToPercent(sliderAlarmVolume);

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

    public void ClickOnMainButton()
    {

    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("MainMenuLevel");
    } 

    public void Restart()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
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

    public void ToPercent(UnityEngine.UI.Slider slider)
    {
        slider.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = Mathf.RoundToInt((slider.value - slider.minValue) / (slider.maxValue - slider.minValue) * 100) + "%";
    }

    public void ChangeMainVolume()
    {
        sfxSlidersAudio.Play();
        audioMixer.SetFloat("Master", SliderToDecibel(sliderMainVolume));
        ToPercent(sliderMainVolume);
        SettingsStore.masterVolume = sliderMainVolume.value;
    }

    public void ChangeSFXVolume()
    {
        sfxSlidersAudio.Play();
        audioMixer.SetFloat("SFX", SliderToDecibel(sliderSfxVolume));
        ToPercent(sliderSfxVolume);
        SettingsStore.sfxVolume = sliderSfxVolume.value;
    }

    public void ChangeMusicVolume()
    {
        sfxSlidersAudio.Play();
        audioMixer.SetFloat("Music", SliderToDecibel(sliderMusicVolume));
        ToPercent(sliderMusicVolume);
        SettingsStore.musicVolume = sliderMusicVolume.value;
    }

    public void ChangeBreathVolume()
    {
        sfxSlidersAudio.Play();
        audioMixer.SetFloat("Breath", SliderToDecibel(sliderBreathVolume));
        ToPercent(sliderBreathVolume);
        SettingsStore.breathVolume = sliderBreathVolume.value;
    }

    public void ChangeAlarmVolume()
    {
        sfxSlidersAudio.Play();
        audioMixer.SetFloat("Alarm", SliderToDecibel(sliderAlarmVolume));
        ToPercent(sliderAlarmVolume);
        SettingsStore.alarmVolume = sliderAlarmVolume.value;
    }

    public void InvertRollAxis()
    {
        sfxSlidersAudio.Play();
        SettingsStore.invertRoll = toggleRollAxis.isOn;
    }

    public void ChangeFOV()
    {
        sfxSlidersAudio.Play();
        FOVText.text = Mathf.Round(sliderFOV.value) + "";
        SettingsStore.baseFov = sliderFOV.value;
    }

    public void ResetSettings()
    {
        SettingsStore.ResetToDefaults();

        audioMixer.SetFloat("Master", SettingsStore.masterVolume);
        audioMixer.SetFloat("Music", SettingsStore.musicVolume);
        audioMixer.SetFloat("SFX", SettingsStore.sfxVolume);
        audioMixer.SetFloat("Breath", SettingsStore.breathVolume);
        audioMixer.SetFloat("Alarm", SettingsStore.alarmVolume);

        GoToSettings();
    }

    public void ChangeVsync()
    {

        int vsync;
        if (toggleUseVsync.isOn)
        {
            vsync = 1;
        }
        else
        {
            vsync = 0;
        }

        QualitySettings.vSyncCount = vsync;
        SettingsStore.useVSync = toggleUseVsync.isOn;
    }
    
    public void InvertJoy()
    {
        SettingsStore.invertJoy = toggleInvertTouch.isOn;
        // will be refreshed by SettingsApply when unpaused
    }

    public void ChangeOutline()
    {
        SettingsStore.disableOutline = toggleDisableOutline.isOn;
    }

    ///////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////



    public void ButtonsSound()
    {
        Debug.Log("Button changed");
        changeButtonAudio.PlayOneShot(changeButtonAudio.clip);
    }

    private float SliderToDecibel(UnityEngine.UI.Slider slider)
    {
        return slider.value <= slider.minValue ? -80f : slider.value;
    }

}
