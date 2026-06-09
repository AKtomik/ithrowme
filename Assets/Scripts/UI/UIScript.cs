using UnityEngine;
using UnityEngine.UI;
public class UIScript : MonoBehaviour
{
    public CapsulePlayer player;

    [Header("Hand Settings")]
    [SerializeField] private Image handImageUI;
    [SerializeField] private Sprite handSpriteReachable;
    [SerializeField] private Sprite handSpriteIdle;
    [SerializeField] private Sprite handSpriteGrab;
    [Header("Cinematic Settings")]
    [SerializeField] private GameObject cinematicBars;
    public float cinematicBarSpeed = 0.7f;

    private bool startCinematic;
    private bool stopCinematic;

    public void EnableCinematicBars()
    {// TODO: bar progressive enter with time parameter
        cinematicBars.transform.localScale = new Vector3(1.25f, 1.25f, 1.25f);
        cinematicBars.SetActive(true);
        startCinematic = true;
    }

    public void DisableCinematicBars()
    {
        stopCinematic = true;
        
        
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject playerGO = GameObject.FindGameObjectWithTag("Player");
        player = playerGO.GetComponent<CapsulePlayer>();
        cinematicBars.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Sprite handSprite;
        if (player.anythingInHand)
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
