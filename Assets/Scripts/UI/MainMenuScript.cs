using UnityEngine;
using UnityEngine.InputSystem;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputActions;
    [Header("Audio")]
    [SerializeField]
    private AudioSource mainMenuTheme;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainMenuTheme.Play();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 1;
        inputActions.FindActionMap("Player").Disable();
        inputActions.FindActionMap("UI").Enable();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
