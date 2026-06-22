using UnityEngine;
using UnityEngine.InputSystem;

public class CheatCode : MonoBehaviour
{
    [SerializeField] public InputActionAsset inputActions;
    private InputAction takeAction;
    private InputAction throwAction;
    
    private CapsuleCheat capsuleCheat;

    private int cheatHoldTakeCount = 0;
    private int cheatHoldThrowCount = 0;

    // setup
    void Awake()
    {
        takeAction = inputActions.FindAction("Player/Take");
        throwAction = inputActions.FindAction("Player/Throw");

        capsuleCheat = GetComponent<CapsuleCheat>();
    }

    void OnEnable()
    {
        throwAction.Enable();
        takeAction.Enable();

        throwAction.performed += OnThrow;
        takeAction.performed += OnTake;
    }
    
    void OnDisable()
    {
        throwAction.Disable();
        takeAction.Disable();

        throwAction.performed -= OnThrow;
        takeAction.performed -= OnTake;
    }

    // perform
    void OnTake(InputAction.CallbackContext ctx)
    {
        if (!throwAction.IsPressed())
        {
            cheatHoldTakeCount = 0;
            return;
        }

        cheatHoldTakeCount += 1;

        if (cheatHoldTakeCount == 5)
        {
            ToggleMoveCheat(true);
            cheatHoldTakeCount = 0;
        }
    }

    
    void OnThrow(InputAction.CallbackContext ctx)
    {
        if (!takeAction.IsPressed())
        {
            cheatHoldThrowCount = 0;
            return;
        }

        cheatHoldThrowCount += 1;

        if (cheatHoldThrowCount == 3)
        {
            // disable all of them
            ToggleMoveCheat(false);
            cheatHoldThrowCount = 0;
        }
    }

    // cheats
    void CheatMode()
    {
        Debug.Log("cheat mode!");
        ReferenceSingleton.instance.bothCanvas.ShowCheat();
    }

    void ToggleMoveCheat(bool enable)
    {
        if (enable) CheatMode();
        Debug.Log("move cheat:"+enable);
        capsuleCheat.enabled = enable;
    }
}
