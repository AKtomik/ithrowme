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
        Vector3 angleSpin = spriteTransform.rotation.eulerAngles;
        Vector3 angleLook = spriteTransform.rotation.eulerAngles + transform.rotation.eulerAngles;
        angleLook = new Vector3(angleLook.x % 360, angleLook.y % 360, angleLook.z %360);

        Vector3 angle = angleSpin + angleLook;
        angle = new Vector3(angle.x % 360, angle.y % 360, angle.z % 360) / 360;

        Sprite angleSprite = spritesImage[(int)((1 - angle.y) * spritesImage.Length)];
        spriteRender.sprite = angleSprite;

        transform.Rotate(new Vector3(0, 3 * Time.deltaTime, 0));
    }
}
