using System;
using UnityEngine;

public class RotateSpriteEffect : MonoBehaviour
{
    [SerializeField] private Sprite[] spritesImage;
    
    [SerializeField] private GameObject spriteObject;
    private Transform spriteTransform;
    private SpriteRenderer spriteRender; 
    private Transform cameraTransform;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteTransform = spriteObject.GetComponent<Transform>();
        spriteRender = spriteObject.GetComponent<SpriteRenderer>();
        cameraTransform = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        float angle = Quaternion.Angle(cameraTransform.rotation, spriteTransform.rotation); 
        Sprite angleSprite = spritesImage[(int)(angle/180 * spritesImage.Length)];
        spriteRender.sprite = angleSprite;
    }
}
