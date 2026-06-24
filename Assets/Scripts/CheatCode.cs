using UnityEngine;
using UnityEngine.InputSystem;

public class CheatCode : MonoBehaviour
{
    [SerializeField] public InputActionAsset inputActions;
    private InputAction takeAction;
    private InputAction throwAction;
    
    private CapsuleCheat capsuleCheat;
    private Rigidbody capsuleBody;

    private int cheatHoldTakeCount = 0;
    private int cheatHoldThrowCount = 0;
    private bool cheatInputing = false;
    
    private bool cheated = false;
    private bool cheating = false;
    private bool moveCheating = false;
    private bool noclipCheating = false;

    // setup
    void Awake()
    {
        takeAction = inputActions.FindAction("Player/Take");
        throwAction = inputActions.FindAction("Player/Throw");

        capsuleCheat = GetComponent<CapsuleCheat>();
        capsuleBody = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        throwAction.Enable();
        takeAction.Enable();

        throwAction.performed += OnThrowPress;
        takeAction.performed += OnTakePress;
        
        throwAction.canceled += OnThrowRelease;
        takeAction.canceled += OnTakeRelease;
    }
    
    void OnDisable()
    {
        throwAction.Disable();
        takeAction.Disable();

        throwAction.performed -= OnThrowPress;
        takeAction.performed -= OnTakePress;
        
        throwAction.canceled -= OnThrowRelease;
        takeAction.canceled -= OnTakeRelease;
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
            Debug.Log("cheat code inputing...");
        }
        else if (cheatHoldTakeCount > 5)
        {
            Debug.Log("cheat code canceled");
        }
    }

    void OnThrowRelease(InputAction.CallbackContext ctx)
    {
        //if (takeAction.IsPressed()) return;

        int cheatIndex = cheatHoldTakeCount;
        cheatHoldTakeCount = 0;

        if (cheatIndex < 3) return;
        Debug.Log("cancel cheat action "+cheatIndex);
        
        switch (cheatIndex)
        {
            //case 2: {
            //    ToggleMoveCheat(false);
            //    ToggleNoclipCheat(false);
            //} break;
            case 3: {
                ToggleMoveCheat(false);
            } break;
            case 4: {
                ToggleNoclipCheat(false);
            } break;

            case 5: {
                cheatInputing = true;
            } break;

            case 6: {
                ToggleNoUI(false);
            } break;
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
        //cheatInputing = false;
    }
    
    void OnTakeRelease(InputAction.CallbackContext ctx)
    {
        if (throwAction.IsPressed()) return;

        int cheatIndex = cheatHoldThrowCount;
        cheatHoldThrowCount = 0;

        if (!cheatInputing) return;
        Debug.Log("perform cheat action "+cheatIndex);

        switch (cheatIndex)
        {
            case 1: {
                PushCheat(10f);
            } break;
            case 2: {
                PushCheat(100f);
            } break;
            case 3: {
                ToggleMoveCheat(true);
            } break;
            case 4: {
                ToggleNoclipCheat(true);
            } break;

            case 5: {
                Debug.Log("take cheat 5");
            } break;
            
            case 6: {
                ToggleNoUI(true);
            } break;
        }
    }

    // cheats
    public void CheatMode()
    {// visual
        cheated = true;
        cheating = moveCheating | noclipCheating;
        Debug.Log("cheat mode "+cheating+"!");
        ReferenceSingleton.instance.bothCanvas.ShowCheat(cheating);
    }
    
    public void PushCheat(float force)
    {
        CheatMode();
        Debug.Log("push cheat:once force of "+force);
        capsuleBody.AddForce(transform.forward * force, ForceMode.Impulse);
    }

    public void ToggleMoveCheat(bool enable)
    {
        if (moveCheating == enable) return;
        moveCheating = enable;
        CheatMode();
        Debug.Log("move cheat:"+enable);
        capsuleCheat.enabled = enable;
    }
    
    public void ToggleNoclipCheat(bool enable)
    {
        if (noclipCheating == enable) return;
        noclipCheating = enable;
        CheatMode();
        Debug.Log("noclip cheat:"+enable);
        ReferenceSingleton.instance.player.SetNoClip(enable);
    }
    
    public void ToggleNoUI(bool enable)
    {
        //CheatMode();// is not really a cheat
        Debug.Log("ui psedo cheat:"+enable);
        ReferenceSingleton.instance.bothCanvas.DiableUI(enable);
    }
}
