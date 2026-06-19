using UnityEngine;

public class ScrollParallax : MonoBehaviour
{
    [SerializeField] private RectTransform canvasChild;
    private GameObject canvasUp;
    private GameObject canvasDown;
    private float dist;
    private Vector3 dir;
    private float progress = 1f;

    public bool scrolling = true;
    public ScrollParallaxDirection scrollDirection = ScrollParallaxDirection.DOWN;
    private ScrollParallaxDirection cachedDirection;
    public float scrollSpeed = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateDirection();

        canvasDown = canvasChild.gameObject;
        canvasDown.transform.position = transform.position;

        canvasUp = Instantiate(canvasChild.gameObject, canvasChild.parent);// [transform] should be equal to [canvasChild.parent]
        canvasUp.transform.position = transform.position + dir * dist;
    }

    void UpdateDirection()
    {
        cachedDirection = scrollDirection;
        switch (scrollDirection)
        {
            case ScrollParallaxDirection.UP: {
                dir = canvasChild.transform.up; 
                dist = canvasChild.rect.height * canvasChild.lossyScale.y;
            } break;
            case ScrollParallaxDirection.DOWN: {
                dir = -canvasChild.transform.up; 
                dist = canvasChild.rect.height * canvasChild.lossyScale.y;
            } break;
            case ScrollParallaxDirection.RIGHT: {
                dir = canvasChild.transform.right; 
                dist = canvasChild.rect.width * canvasChild.lossyScale.y;
            } break;
            case ScrollParallaxDirection.LEFT: {
                dir = -canvasChild.transform.right; 
                dist = canvasChild.rect.width * canvasChild.lossyScale.y;
            } break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!scrolling) return;

        if (cachedDirection != scrollDirection) UpdateDirection();
        
        progress += Time.deltaTime / 10 * scrollSpeed;
        if (progress > 1)
        {
            progress = 0;
        }
        if (progress < 0)
        {
            progress = 1;
        }

        canvasDown.transform.position = transform.position - ((1 - progress) * dist * dir);
        canvasUp.transform.position = transform.position + (progress * dist * dir);
    }
}

public enum ScrollParallaxDirection
{
    UP,
    DOWN,
    LEFT,
    RIGHT,
}