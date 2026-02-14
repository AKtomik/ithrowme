using System;
using UnityEngine;

public class RotateSpriteEffect : MonoBehaviour
{
    [SerializeField] private Sprite[] spritesImage;
    
    [SerializeField] private GameObject spriteObject;
    private Transform spriteTransform;
    private SpriteRenderer spriteRender;

    public Vector3 continusRotate = Vector3.zero;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteTransform = spriteObject.GetComponent<Transform>();
        spriteRender = spriteObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 angleLook = spriteTransform.rotation.eulerAngles - transform.rotation.eulerAngles + Vector3.one * 360;
        angleLook = new Vector3(angleLook.x % 360, angleLook.y % 360, angleLook.z % 360) / 360;

        Sprite angleSprite = spritesImage[(int)(angleLook.y * spritesImage.Length)];
        spriteRender.sprite = angleSprite;

        if (!continusRotate.Equals(Vector3.zero))
            transform.Rotate(continusRotate * Time.deltaTime);
    }
}
