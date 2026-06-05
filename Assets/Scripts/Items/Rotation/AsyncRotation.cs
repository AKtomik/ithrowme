using System;
using UnityEngine;

public class AsyncRotation : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private Transform renderTransform;
    [SerializeField] private Transform realTransform;

    [ToggleGroup("Stair Effect")]
    [SerializeField] private bool stairRotation = false;
    [SerializeField, DrawIf("stairRotation")] private bool lookingCamera = true;
    [SerializeField, DrawIf("stairRotation")] private Vector3Int rotaSteps = new(4, 4, 4);
    private Transform cameraTransform;
    private Vector3 rotaInterval;

    [ToggleGroup("Laggy Effect")]
    [SerializeField] private bool laggyRotation = false;
    [SerializeField, DrawIf("laggyRotation")] private float renderFps = 10;
    private float renderInterval;
    private Rigidbody realBody;
    private float renderTime = 0;

    bool IsBlocked()
    {
        return realBody && realBody.isKinematic;
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (realTransform.gameObject.TryGetComponent<Rigidbody>(out var rb))
        {
            realBody = rb;
        } else {
            Debug.LogWarning("can't get Rigidbody of realTransform");
        }

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
        if (laggyRotation && !IsBlocked())
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
        if (stairRotation && !IsBlocked())
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
