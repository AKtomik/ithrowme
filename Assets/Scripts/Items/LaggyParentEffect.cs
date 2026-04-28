using UnityEngine;

public class LaggyParentEffect : MonoBehaviour
{
    
    [SerializeField] private Transform renderTransform;
    [SerializeField] private Transform realTransform;

    public float renderFps = 10;
    private float renderInterval;
    private float renderTime = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        renderInterval = 1 / renderFps;
    }

    // Update is called once per frame
    void Update()
    {
        renderTime += Time.deltaTime;
        if (renderTime > renderInterval)
        {
            RefreshRender();
            renderTime = 0;
        }
        UpdateRender();
    }

    void UpdateRender()
    {
        renderTransform.position = realTransform.position;
    }    

    void RefreshRender()
    {
        renderTransform.localRotation  = realTransform.localRotation;
    }
}
