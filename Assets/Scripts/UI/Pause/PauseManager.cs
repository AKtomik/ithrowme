using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PauseManager : MonoBehaviour
{
    // /!\ PauseManager's GameObject MUST have the tag "PauseManager" so that PauseMenuScript works /!\

    [SerializeField] private bool blockPauseInCinematic = true;
    [SerializeField] private InputActionAsset inputActions;
    [SerializeField] private Canvas pauseCanvas;
    [SerializeField] private RawImage screenImage;
    [SerializeField] private GameObject[] gameParents;
    [SerializeField] private CinematicManager cinematicManager;
    public bool isEnding = false;
    private InputAction pauseAction;

    private bool pauseState;

    void Awake()
    {
        pauseAction = inputActions.FindAction("State/Pause");
        gameObject.tag = "PauseManager";
        Resume();
    }

    void OnEnable()
	{
        pauseAction.Enable();

		pauseAction.performed += TogglePause;
	}
    
	void OnDisable()
	{
		pauseAction.performed -= TogglePause;
        
        pauseAction.Disable();
	}

    public bool IsPaused()
    {
        return pauseState;
        
    }

    public void AskPause()
    {
        if (blockPauseInCinematic && cinematicManager.IsCinematic() || isEnding) return;
        Debug.Log("pausing...");
        StartCoroutine(PauseRoutine());
    }
    
    private IEnumerator PauseRoutine()
    {
        yield return new WaitForEndOfFrame();

        Texture2D screenshotTexture = ScreenCapture.CaptureScreenshotAsTexture();
        screenImage.texture = screenshotTexture;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;

        PauseSignal.RaisePause();

        foreach (var loopGameObject in gameParents)
            loopGameObject.SetActive(false);
        
        pauseCanvas.gameObject.SetActive(true);
        pauseState = true;
        inputActions.FindActionMap("Player").Disable();
        inputActions.FindActionMap("UI").Enable();
        
        Debug.Log("paused!");
    }

    public void Resume()
    {
        Debug.Log("resume");

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;

        inputActions.FindActionMap("Player").Enable();
        inputActions.FindActionMap("UI").Disable();

        foreach (var loopGameObject in gameParents)
            loopGameObject.SetActive(true);

        pauseCanvas.gameObject.SetActive(false);
        pauseState = false;

        EventSystem.current.SetSelectedGameObject(null);
        
        PauseSignal.RaiseResume();
    }

    public void SetPaused(bool paused)
    {
        if (paused) AskPause();
        else Resume();
    }


    void TogglePause(InputAction.CallbackContext ctx)
    {
		SetPaused(!IsPaused());
	}
}
