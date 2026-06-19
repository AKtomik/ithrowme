using UnityEngine;

public class ScrollParallax : MonoBehaviour
{
    [SerializeField] private RectTransform canvasChild;
    private GameObject canvasUp;
    private GameObject canvasDown;
    private float height;
    private float progress = 1f;
    private int step;

    [SerializeField] private bool scrolling = true;
    [SerializeField] private float scrollSpeed = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        height = canvasChild.rect.height * canvasChild.lossyScale.y;

        canvasDown = canvasChild.gameObject;
        canvasDown.transform.position = transform.position;

        canvasUp = Instantiate(canvasChild.gameObject, canvasChild.parent);// [transform] should be equal to [canvasChild.parent]
        canvasUp.transform.position = transform.position - canvasChild.transform.up * height;
    }

    // Update is called once per frame
    void Update()
    {
        if (!scrolling) return;
        
        progress += Time.deltaTime / 10 * scrollSpeed;
        if (progress > 1)
        {
            progress = 0;
        }

        canvasDown.transform.position = transform.position - ((1 - progress) * height * canvasDown.transform.up);
        canvasUp.transform.position = transform.position + (progress * height * canvasDown.transform.up);
    }
}