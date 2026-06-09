using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    private InputAction playerInput;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        playerInput = GetComponent<InputAction>();
            
    }
}
