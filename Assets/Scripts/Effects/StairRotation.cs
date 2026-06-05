using System;
using UnityEngine;

public class StairRotationEffect : MonoBehaviour
{
    
    [SerializeField] private Transform renderTransform;
    [SerializeField] private Transform realTransform;

    [SerializeField] public bool lookingCamera = true;
    [SerializeField] public Vector3Int rotaSteps = new(4, 4, 4);
    
    private Transform cameraTransform;
    private Vector3 rotaInterval;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cameraTransform = Camera.main.transform;
        rotaInterval = new Vector3(
            360 / rotaSteps.x,
            360 / rotaSteps.y,
            360 / rotaSteps.z
            );
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
        if (lookingCamera)
            renderTransform.LookAt(cameraTransform);
        else
            renderTransform.localRotation = Quaternion.Euler(Vector3.zero);
        Vector3 diffRotation = realTransform.localRotation.eulerAngles - renderTransform.localRotation.eulerAngles;
        //Debug.Log("raw diffRotation:"+diffRotation.x+";"+diffRotation.y+";"+diffRotation.z);
        diffRotation = new Vector3(
            (float)Math.Round(diffRotation.x / rotaInterval.x) * rotaInterval.x,
            (float)Math.Round(diffRotation.y / rotaInterval.y) * rotaInterval.y,
            (float)Math.Round(diffRotation.z / rotaInterval.z) * rotaInterval.z
            );
        //Debug.Log("round diffRotation:"+diffRotation.x+";"+diffRotation.y+";"+diffRotation.z);
        
        renderTransform.localRotation = Quaternion.Euler(renderTransform.localRotation.eulerAngles + diffRotation);
    }
}
