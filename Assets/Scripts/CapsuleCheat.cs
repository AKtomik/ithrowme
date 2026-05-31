using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class CapsuleCheat : MonoBehaviour
{
    public float speedPosBySecond = 10f;
    public float velocityBodyBySecond = 3f;

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
        float delta = Time.deltaTime;
        Vector3 move = new Vector3();

        if (leftAction.IsPressed())
            move.x -= delta;
        if (rightAction.IsPressed())
            move.x += delta;
        if (frontAction.IsPressed())
            move.z += delta;
        if (behindAction.IsPressed())
            move.z -= delta;
        if (upAction.IsPressed())
            move.y += delta;
        if (downAction.IsPressed())
            move.y -= delta;
        
        if (stopAction.IsPressed())
        {
            body.linearVelocity = Vector3.zero;
            body.angularVelocity = Vector3.zero;
        }

        transform.position += transform.rotation * move * speedPosBySecond;
        body.AddForce(transform.rotation * move * velocityBodyBySecond * 100, ForceMode.Acceleration);
    }
}
