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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainMenuTheme.Play();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 1;
        inputActions.FindActionMap("Player").Disable();
        inputActions.FindActionMap("UI").Enable();
        topAnimator.SetTrigger("One");
        Invoke("StartMenu", 14f);
    }


    private void StartMenu()
    {
        topAnimator.SetTrigger("FadeOut");
    }
    private void DeleteAll()
    {
        Destroy(topAnimator.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
