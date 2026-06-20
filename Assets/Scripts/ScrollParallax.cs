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
    public float initialProgress = 1f;
    public float scrollSpeed = 1f;
    public bool loop = true;

    private float scrollComputedSpeed = 1f; 
    public float ScrollParentSpeed
    {
        set
        {
            scrollComputedSpeed = scrollSpeed * value;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateDirection();// at first

        progress = initialProgress;

        canvasDown = canvasChild.gameObject;
        canvasDown.transform.position = transform.position;

        canvasUp = Instantiate(canvasChild.gameObject, canvasChild.parent);// [transform] should be equal to [canvasChild.parent]
        canvasUp.transform.position = transform.position + dir * dist;
        
        UpdatePosition();// at last
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
        
        progress += Time.deltaTime / 10 * scrollComputedSpeed;
        if (progress > 1)
        {
            if (loop) progress = 0;
            else scrolling = false;
        }
        if (progress < 0)
        {
            if (loop) progress = 1;
            else scrolling = false;
        }

        UpdatePosition();
    }
    
    void UpdatePosition()
    {
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