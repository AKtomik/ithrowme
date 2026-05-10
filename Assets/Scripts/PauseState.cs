using UnityEngine;
using UnityEngine.InputSystem;

public class PauseState : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputActions;
    private InputAction pauseAction;

    private bool paused;

    void Awake()
    {
        pauseAction = inputActions.FindAction("State/Pause");

        //SetPaused(false);
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
        return paused;
    }
    
    public void SetPaused(bool newPaused)
    {
        Debug.Log("setpaused:"+newPaused.ToString());
        gameObject.SetActive(newPaused);
        
        if (newPaused)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        paused = newPaused;
    }

    void TogglePause(InputAction.CallbackContext ctx)
    {
        Debug.Log("TogglePause.");
        SetPaused(!IsPaused());
    }

	// Update is called once per frame
	void Update()
    {
        
    }
}
