using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    // /!\ PauseManager's GameObject MUST have the tag "PauseManager" so that PauseMenuScript works /!\

    [SerializeField] private InputActionAsset inputActions;
    [SerializeField] private Canvas pauseCanvas;
    [SerializeField] private RawImage screenImage;
    [SerializeField] private GameObject[] gameParents;
    private InputAction pauseAction;

    private bool pauseState;

    void Awake()
    {
        pauseAction = inputActions.FindAction("State/Pause");

        Unpause();
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

        foreach (var loopGameObject in gameParents)
            loopGameObject.SetActive(false);
        pauseCanvas.gameObject.SetActive(true);
        pauseState = true;
        Debug.Log("paused!");
    }

    public void Unpause()
    {
        Debug.Log("unpause");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;

        foreach (var loopGameObject in gameParents)
            loopGameObject.SetActive(true);
        pauseCanvas.gameObject.SetActive(false);
        pauseState = false;
    }

    public void SetPaused(bool paused)
    {
        if (paused) AskPause();
        else Unpause();
    }


    void TogglePause(InputAction.CallbackContext ctx)
    {
		SetPaused(!IsPaused());
	}
}
