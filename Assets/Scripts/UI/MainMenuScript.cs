using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputActions;
    [Header("Audio")]
    [SerializeField] private AudioSource mainMenuTheme;
    [Header("UI")]
    [SerializeField] private Animator topAnimator;
    [SerializeField] private PauseMenuScript menuScript;

    private InputAction click;
    private bool isStarted = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainMenuTheme.Play();
        click = inputActions.FindAction("Pause");

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 1;
        inputActions.FindActionMap("Player").Disable();
        inputActions.FindActionMap("UI").Enable();
        topAnimator.SetTrigger("One");
        Invoke("StartMenu", 13.5f);
        
    }


    private void StartMenu()
    {
        isStarted = true;
        topAnimator.SetTrigger("FadeOut");
        Invoke("DeleteAll", 1f);
    }
    private void DeleteAll()
    {
        menuScript.GoToMainCanva();
        Destroy(topAnimator.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (click.WasPressedThisFrame() && !isStarted)
        {
            StartMenu();
            CancelInvoke();
        }
    }
}
