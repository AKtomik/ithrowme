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
    private bool cheatInputing = false;

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

        throwAction.performed += OnThrowPress;
        takeAction.performed += OnTakePress;
    }
    
    void OnDisable()
    {
        throwAction.Disable();
        takeAction.Disable();

        throwAction.performed -= OnThrowPress;
        takeAction.performed -= OnTakePress;
    }

    // perform
    void ResetSteak()
    {
        cheatHoldTakeCount = 0;
        cheatHoldThrowCount = 0;
        cheatInputing = false;
    }

    void OnTakePress(InputAction.CallbackContext ctx)
    {
        if (!throwAction.IsPressed())
        {
            ResetSteak();
            return;
        }

        cheatHoldTakeCount += 1;
        cheatHoldThrowCount = 0;
        cheatInputing = false;

        if (cheatHoldTakeCount == 5)
        {
            //if (cheatHoldTakeCount != 0)
            //{
            //    ResetSteak();
            //    return;
            //}
            //cheatInputing = true;
            ToggleMoveCheat(true);
            ResetSteak();
        } 
        else if (cheatHoldTakeCount > 5)
        {
            ResetSteak();
        }
    }

    
    void OnThrowPress(InputAction.CallbackContext ctx)
    {
        if (!takeAction.IsPressed())
        {
            ResetSteak();
            return;
        }

        cheatHoldThrowCount += 1;
        cheatHoldTakeCount = 0;
        cheatInputing = false;

        if (cheatHoldThrowCount == 3)
        {
            // disable all of them
            ToggleMoveCheat(false);
            CheatMode(false);
            ResetSteak();
        }
    }

    // cheats
    void CheatMode(bool enable)
    {// visual
        Debug.Log("cheat mode "+enable+"!");
        ReferenceSingleton.instance.bothCanvas.ShowCheat(enable);
    }

    void ToggleMoveCheat(bool enable)
    {
        if (enable) CheatMode(true);
        Debug.Log("move cheat:"+enable);
        capsuleCheat.enabled = enable;
    }
}
