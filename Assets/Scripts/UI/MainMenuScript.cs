using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputActions;
    [Header("Audio")]
    [SerializeField] private AudioSource mainMenuTheme;
    [SerializeField] private AudioSource cinematicTheme;
    [Header("UI")]
    [SerializeField] private Animator topAnimator;
    [SerializeField] private PauseMenuScript menuScript;
    [SerializeField] private RawImage cinematicBlack;

    private InputAction click;
    private bool isStarted = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        mainMenuTheme.Play();
        cinematicBlack.gameObject.SetActive(false);
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


    public void StartGame()
    {
        mainMenuTheme.Stop();
        cinematicBlack.gameObject.SetActive(true);
        cinematicTheme.Play();
        StartCoroutine(WaitForCinematicEnd());
    }

    private IEnumerator WaitForCinematicEnd()
    {
        yield return new WaitWhile(() => cinematicTheme.isPlaying);
        OnCinematicThemeFinished();
    }

    private void OnCinematicThemeFinished()
    {
        SceneManager.LoadScene("BaseLevel");
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1)
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
