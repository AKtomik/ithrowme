using UnityEngine;

public class StairRotationEffect : MonoBehaviour
{
    
    [SerializeField] private Transform renderTransform;
    [SerializeField] private Transform realTransform;

    public bool lookingCamera = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePosition();
        UpdateRotation();
    }

    void UpdatePosition()
    {
        renderTransform.position = realTransform.position;
    }

    void UpdateRotation()
    {
        renderTransform.rotation  = realTransform.rotation;
    }
}
