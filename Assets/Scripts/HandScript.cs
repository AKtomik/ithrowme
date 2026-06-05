using UnityEngine;
using UnityEngine.UI;
public class HandScript : MonoBehaviour
{
    public CapsulePlayer player;

    [Header("Hand Settings")]
    [SerializeField] private Image handImageUI;
    [SerializeField] private Sprite handSpriteReachable;
    [SerializeField] private Sprite handSpriteIdle;
    [SerializeField] private Sprite handSpriteGrab;

    private bool anythingInHand = false;

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
        if (anythingInHand)
            handSprite = handSpriteGrab;
        else if (player.reachableObject)
            handSprite = handSpriteReachable;
        else
            handSprite = handSpriteIdle;
        handImageUI.sprite = handSprite;
    }
}
