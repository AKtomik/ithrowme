using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class CapsuleCheat : MonoBehaviour
{
    public float speedBySecond = 1f;

    [SerializeField] private InputActionAsset inputActions;
    private InputAction leftAction;//-x
    private InputAction rightAction;//+x
    private InputAction frontAction;//+z
    private InputAction behindAction;//-z
    private InputAction upAction;//+y
    private InputAction downAction;//-y
    private InputAction stopAction;//-y
    
    private Rigidbody body;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        leftAction = inputActions.FindAction("Cheat/Left");
        rightAction = inputActions.FindAction("Cheat/Right");
        frontAction = inputActions.FindAction("Cheat/Front");
        behindAction = inputActions.FindAction("Cheat/Behind");
        upAction = inputActions.FindAction("Cheat/Up");
        downAction = inputActions.FindAction("Cheat/Down");
        stopAction = inputActions.FindAction("Cheat/Stop");

        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float speed = speedBySecond * Time.deltaTime;
        Vector3 move = new Vector3();

        if (leftAction.IsPressed())
            move.x -= speed;
        if (rightAction.IsPressed())
            move.x += speed;
        if (frontAction.IsPressed())
            move.z += speed;
        if (behindAction.IsPressed())
            move.z -= speed;
        if (upAction.IsPressed())
            move.y += speed;
        if (downAction.IsPressed())
            move.y -= speed;
        
        if (stopAction.IsPressed())
        {
            body.linearVelocity = Vector3.zero;
            body.angularVelocity = Vector3.zero;
        }

        transform.position += transform.rotation * move;
    }
}
