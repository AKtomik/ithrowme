using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputActions;
    [SerializeField] private Canvas pauseCanvas;
    [SerializeField] private RawImage screenImage;
    [SerializeField] private GameObject gameParent;
    private InputAction pauseAction;

    private bool pauseState;

    void Awake()
    {
        pauseAction = inputActions.FindAction("State/Pause");

        StartCoroutine(SetPaused(false));
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
    
    public IEnumerator SetPaused(bool paused)
    {
        Debug.Log("pausing..."+paused);
        yield return new WaitForEndOfFrame();

        if (paused)
        {
            Texture2D screenshotTexture = ScreenCapture.CaptureScreenshotAsTexture();
            screenImage.texture = screenshotTexture;
            
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1;
        }
        
        //gameParent.SetActive(!paused);
        pauseCanvas.gameObject.SetActive(paused);
        pauseState = paused;
        Debug.Log("paused!");
    }

    void TogglePause(InputAction.CallbackContext ctx)
    {
		StartCoroutine(SetPaused(!IsPaused()));
	}
}
