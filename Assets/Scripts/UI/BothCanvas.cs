using UnityEngine;
using UnityEngine.UI;

public class BothCanvas : MonoBehaviour
{
    public CapsulePlayer player;

    [Header("Hand Settings")]
    [SerializeField] private Image handImageUI;
    // unity editor can't take dictionnary (wanted enum - sprite dictionnary)
    // so it is the way to go for now
    [SerializeField] private Sprite handSpriteReachable;
    [SerializeField] private Sprite handSpriteIdle;
    [SerializeField] private Sprite handSpriteGrab;
    [SerializeField] private Sprite handSpriteFinger;
    [SerializeField] private HandState defaultCinematicHandState = HandState.IDLE;
    
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
            }
        }
    }
}

public enum HandState
{
    IDLE = 0,
    REACHABLE = 1,
    GRAB = 2,
    FINGER = 3,
}