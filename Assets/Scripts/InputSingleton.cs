using UnityEngine;
using UnityEngine.InputSystem;

public class InputSingleton : MonoBehaviour
{
    public static InputSingleton instance;

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
