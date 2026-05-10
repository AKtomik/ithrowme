using UnityEngine;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputActions;
    [SerializeField] private Canvas pauseCanvas;
    private InputAction pauseAction;

    private bool pauseState;

    void Awake()
    {
        pauseAction = inputActions.FindAction("State/Pause");

        SetPaused(false);
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
    
    public void SetPaused(bool paused)
    {
        pauseCanvas.gameObject.SetActive(paused);
        
        if (paused)
        {
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
        pauseState = paused;
    }

    void TogglePause(InputAction.CallbackContext ctx)
    {
        SetPaused(!IsPaused());
    }
}
