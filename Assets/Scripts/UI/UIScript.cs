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
    [SerializeField] private GameObject cinematicBars;
    

    public void EnableCinematic()
    {// TODO: bar progressive enter with time parameter
        cinematicBars.SetActive(true);
        
    }

    public void DisableCinematic()
    {
        cinematicBars.SetActive(false);
        
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject playerGO = GameObject.FindGameObjectWithTag("Player");
        player = playerGO.GetComponent<CapsulePlayer>();
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
    }
}
