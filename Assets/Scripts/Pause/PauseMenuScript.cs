using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
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
    [SerializeField] private GameObject quitCanva;
    [SerializeField] private GameObject gamepadCanva;
    [SerializeField] private GameObject keyboardCanva;

    [Header("Settings parameters")]
    [SerializeField] private Slider sliderLookSensitivity;
    [SerializeField] private Slider sliderRollSensitivity;
    [SerializeField] private Slider sliderSfxVolume;
    [SerializeField] private Slider sliderMainVolume;


    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioSource changeButtonAudio;
    
    [SerializeField] private AudioSource sfxSlidersAudio;
    

    private InputAction navigateActions;
    private GameObject previousSelectedGameobject;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GoToMainCanva();
        pauseManagerGO = GameObject.FindGameObjectWithTag("PauseManager");
        pauseManager = pauseManagerGO.GetComponent<PauseManager>();

        navigateActions = inputActions.FindAction("UI/Navigate");
        navigateActions.Enable();
        navigateActions.performed += Navigate;
    }
    private void OnEnable()
    {
        GoToMainCanva();
        previousSelectedGameobject = EventSystem.current.currentSelectedGameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (previousSelectedGameobject != EventSystem.current.currentSelectedGameObject)
        {
            ButtonsSound();
            previousSelectedGameobject = EventSystem.current.currentSelectedGameObject;
        }
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
        SelectFirstButton();
    }

    public void ClickOnQuitButton()
    {
        mainCanva.SetActive(false);
        quitCanva.SetActive(true);
        SelectFirstButton();
    }


    public void GoToSettings()
    {
        mainCanva.SetActive(false);
        gamepadCanva.SetActive(false);
        keyboardCanva.SetActive(false);

        settingsCanva.SetActive(true);

        sliderLookSensitivity.value = SettingsStore.lookSensivity;
        sliderRollSensitivity.value = SettingsStore.rollSensivity;
        
        audioMixer.GetFloat("SFX", out float sfxVolume);
        sliderSfxVolume.value = sfxVolume;

        SelectFirstButton();
    }

    public void GoToGamepad()
    {
        settingsCanva.SetActive(false);
        gamepadCanva.SetActive(true);
    }

    public void GoToKeyboard()
    {
        settingsCanva.SetActive(false);
        keyboardCanva.SetActive(true);
    }

    ///////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////

    public void SelectFirstButton()
    {
        GameObject currentCanva = GameObject.FindGameObjectWithTag("PauseCanva"); // works because it finds only ACTIVE ones
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(currentCanva.transform.GetChild(1).gameObject);
    }

    public void Resume()
    {
        pauseManager.Unpause(); // If failed : PauseManager must have the tag "PauseManager"

    }

    public void QuitGame()
    {
        Debug.Log("Game is quitting...");
        Application.Quit();
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
    }
    public void ChangeMainVolume()
    {
        sfxSlidersAudio.Play();
        audioMixer.SetFloat("Main", sliderMainVolume.value);
    }

    ///////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////

    void Navigate(InputAction.CallbackContext ctx)
    {
        
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            SelectFirstButton();
        }
    }
    
    void ButtonsSound()
    {
        changeButtonAudio.Play();
    }

}
