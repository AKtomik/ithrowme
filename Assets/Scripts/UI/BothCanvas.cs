using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static System.Collections.Specialized.BitVector32;


public class BothCanvas : MonoBehaviour
{
    public CapsulePlayer player;

    [SerializeField] private GameObject textsParent;
    [SerializeField] private TextMeshProUGUI speedText;
    [SerializeField] private TextMeshProUGUI restartText;
    [SerializeField] private InputActionReference KeyboardReset;
    [SerializeField] private InputActionReference GamepadReset;
    
    [Header("Hand Settings")]
    [SerializeField] private Image handImageUI;
    // unity editor can't take dictionnary (wanted enum - sprite dictionnary)
    // so it is the way to go for now
    [SerializeField] private Sprite handSpriteReachable;
    [SerializeField] private Sprite handSpriteIdle;
    [SerializeField] private Sprite handSpriteGrab;
    [SerializeField] private Sprite handSpriteFinger;
    [SerializeField] private HandState defaultCinematicHandState = HandState.IDLE;
    [SerializeField] private bool cinematicHandDoVisualItemGrab = true;
    
    [Header("Crosshair Settings")]
    [SerializeField] private Image CrosshairImageUI;

    [Header("Cinematic Settings")]
    [SerializeField] private GameObject cinematicBars;
    [SerializeField] private GameObject missionsContainer;
    public float cinematicBarSpeed = 0.7f;

    private bool startCinematic;
    private bool stopCinematic;
    private bool inCinematic;
    private HandState cinematicHandState;

    [Header("Info Settings")]
    [SerializeField] private TextMeshProUGUI cheatText;

    

    // cinematic bars
    public void StartCinematic()
    {
        // cinematic state
        inCinematic = true;
        missionsContainer.SetActive(false);
        CrosshairImageUI.gameObject.SetActive(false);
        // cinematic bars
        cinematicBars.transform.localScale = new Vector3(1.25f, 1.25f, 1.25f);
        cinematicBars.SetActive(true);
        startCinematic = true;
        // cinematic hand
        cinematicHandState = defaultCinematicHandState;
    }

    public void EndCinematic()
    {
        // cinematic state
        inCinematic = false;
        missionsContainer.SetActive(true);
        CrosshairImageUI.gameObject.SetActive(true);
        // cinematic bars
        stopCinematic = true;
    }

    // cheat
    public void ShowCheat(bool enable)
    {
        cheatText.gameObject.SetActive(true);
        cheatText.text = enable ? "CHEAT MODE" : "CHEATED";
    }
    
    public void DiableUI(bool enable)
    {
        textsParent.SetActive(!enable);
        CrosshairImageUI.enabled = !enable;
    }
    
    // hand
    Sprite HandStateSprite(HandState state)
    {
        return state switch
        {
            HandState.IDLE => handSpriteIdle,
            HandState.REACHABLE => handSpriteReachable,
            HandState.GRAB => handSpriteGrab,
            HandState.FINGER => handSpriteFinger,
            _ => handSpriteIdle,
        };
    }
    
    public void ChangeCinematicHandState(HandState state)
    {
        cinematicHandState = state;
    }

    // execution
    void Start()
    {
        GameObject playerGO = GameObject.FindGameObjectWithTag("Player");
        player = playerGO.GetComponent<CapsulePlayer>();
        cinematicBars.SetActive(false);
    }

    void Update()
    {
        Sprite handSprite;
        if (inCinematic)
            if (cinematicHandDoVisualItemGrab && player.anythingInHand)
                handSprite = handSpriteGrab;
            else
                handSprite = HandStateSprite(cinematicHandState);
        else if (player.anythingInHand)
            handSprite = handSpriteGrab;
        else if (player.reachableObject)
            handSprite = handSpriteReachable;
        else
            handSprite = handSpriteIdle;
        handImageUI.sprite = handSprite;

        if (startCinematic)
        {
            if (cinematicBars.transform.localScale.x > 1)
            {
                cinematicBars.transform.localScale -= new Vector3(cinematicBarSpeed, cinematicBarSpeed, cinematicBarSpeed) * Time.deltaTime;
            }
            else
            {
                speedText.gameObject.SetActive(false);
                startCinematic = false;
            }
        }

        if (stopCinematic)
        {
            if (cinematicBars.transform.localScale.x < 1.25f)
            {
                cinematicBars.transform.localScale += new Vector3(cinematicBarSpeed, cinematicBarSpeed, cinematicBarSpeed) * Time.deltaTime;
            }
            else
            {
                cinematicBars.SetActive(false);
                stopCinematic = false;
                speedText.gameObject.SetActive(true);
            }
        }


        InputSystem.onActionChange += player.OnInputChange;

        
        if (player.isKeyboard)
        {
            restartText.text = player.inputActions.FindAction("Player/Reset").GetBindingDisplayString(0) + " pour recommencer";
            
        }
        else
        {
            restartText.text = player.inputActions.FindAction("Player/Reset").GetBindingDisplayString(1) + " pour recommencer";
            
        }
        

        speedText.text = Math.Round(player.playerBody.linearVelocity.magnitude, 2) + " m/s";
    }
}

public enum HandState
{
    IDLE = 0,
    REACHABLE = 1,
    GRAB = 2,
    FINGER = 3,
}