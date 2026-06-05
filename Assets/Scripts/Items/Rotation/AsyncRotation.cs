using System;
using UnityEngine;

public class AsyncRotation : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private Transform renderTransform;
    [SerializeField] private Transform realTransform;

    [Header("Stair Effect")]
    [SerializeField] private bool stairRotation = false;
    [SerializeField] private bool lookingCamera = true;
    [SerializeField] private Vector3Int rotaSteps = new(4, 4, 4);
    private Transform cameraTransform;
    private Vector3 rotaInterval;

    [Header("Laggy Effect")]
    [SerializeField] private bool laggyRotation = false;
    [SerializeField] private float renderFps = 10;
    private float renderInterval;
    private float renderTime = 0;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (stairRotation)
        {
            cameraTransform = Camera.main.transform;
            rotaInterval = new Vector3(
                360 / rotaSteps.x,
                360 / rotaSteps.y,
                360 / rotaSteps.z
                );
        }

        if (laggyRotation)
        {
            renderInterval = 1 / renderFps;
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePosition();
        if (laggyRotation)
        {
            renderTime += Time.deltaTime;
            if (renderTime > renderInterval)
            {
                UpdateRotation();
                renderTime = 0;
            }
        } else {
            UpdateRotation();
        }
    }

    void UpdatePosition()
    {
        renderTransform.position = realTransform.position;
    }

    void UpdateRotation()
    {
        if (stairRotation)
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
        else
        {
            renderTransform.localRotation = realTransform.localRotation;
        }
    }
}
