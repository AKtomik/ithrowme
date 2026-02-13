using UnityEngine;

public class BillboardSpriteEffect : MonoBehaviour
{
    private Transform cameraTransform;
    public Transform spriteTransform;

    public bool lookX = true;
    public bool lookY = true;
    public bool lookZ = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cameraTransform = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        spriteTransform.LookAt(cameraTransform);
        spriteTransform.rotation = Quaternion.Euler(
            lookX ? spriteTransform.rotation.eulerAngles.x : 0,
            lookY ? spriteTransform.rotation.eulerAngles.y : 0,
            lookZ ? spriteTransform.rotation.eulerAngles.z : 0
            );
    }
}
