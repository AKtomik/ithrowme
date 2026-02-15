using UnityEngine;
using UnityEngine.InputSystem;

public class CapsulePlayerController : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private InputActionAsset inputActions;
    private InputAction lookAction;
    private InputAction takeAction;
    private InputAction throwAction;
    private InputAction dashAction;

    [Header("Look Settings")]
    [SerializeField] private Transform playerPivot;
    [SerializeField] private float sensitivity = .5f;
    [SerializeField] private float smoothTime = .5f;
    [SerializeField] private float rotationMaxSpeed = 1000f;

    [Header("Throw Settings")]
    [SerializeField] private GameObject throwablePrefab;
    [SerializeField] private Transform throwPoint;
    [SerializeField] private float throwObjectForce = 15f;
    [SerializeField] private float throwPlayerForce = -15f;

    [Header("Dash Settings")]
    [SerializeField] private float dashForce = 20f;

    private Rigidbody rb;
    private float rotateEnterCooldown = 1f;
    private Quaternion rotationRaw;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        lookAction = inputActions.FindAction("Player/Look");
        takeAction = inputActions.FindAction("Player/Take");
        throwAction = inputActions.FindAction("Player/Throw");
        dashAction = inputActions.FindAction("Player/Dash");
        
        rotationRaw = playerPivot.localRotation;
    }

    void OnEnable()
    {
        lookAction.Enable();
        throwAction.Enable();
        takeAction.Enable();
        dashAction.Enable();

        throwAction.performed += OnThrow;
        takeAction.performed += OnTake;
        dashAction.performed += OnDash;
    }

    void OnDisable()
    {
        throwAction.performed -= OnThrow;
        takeAction.performed -= OnTake;
        dashAction.performed -= OnDash;

        lookAction.Disable();
        throwAction.Disable();
        takeAction.Disable();
        dashAction.Disable();
    }

    void Update()
    {
        HandleLook();
    }

    void HandleLook()
    {
        Vector2 mouseInput = lookAction.ReadValue<Vector2>() * sensitivity * Time.deltaTime;        
        if (mouseInput.magnitude > 0.01)
        {
            if (rotateEnterCooldown > 0)
            {
                rotateEnterCooldown -= 1;
            }
            else if (takeAction.IsPressed())
            {
                Quaternion angleZ = Quaternion.AngleAxis(mouseInput.x, rotationRaw * Vector3.forward);
                rotationRaw = angleZ * rotationRaw;
                Debug.Log("b:"+rotationRaw);
            } else {
                Quaternion angleX = Quaternion.AngleAxis(mouseInput.x, rotationRaw * Vector3.up);
                rotationRaw = angleX * rotationRaw;
                Quaternion angleY = Quaternion.AngleAxis(-mouseInput.y, rotationRaw * Vector3.right);
                rotationRaw = angleY * rotationRaw;
                Debug.Log("a:"+rotationRaw);
            }
        }
        //rotationSmooth = Vector3.SmoothDamp(rotationSmooth, rotationRaw, ref rotationVelocity, smoothTime, rotationMaxSpeed, Time.deltaTime);
        playerPivot.localRotation = rotationRaw;
    }

    void OnTake(InputAction.CallbackContext ctx)
    {
        Debug.Log("player take action");
    }

    void OnThrow(InputAction.CallbackContext ctx)
    {
        Debug.Log("player throw action");
        GameObject newProjectile = Instantiate(throwablePrefab, throwPoint.position, Quaternion.identity);
        newProjectile.transform.rotation = throwPoint.rotation;
        
        const int size = 1;
        newProjectile.transform.localScale *= size;
        
        newProjectile.GetComponent<MeshRenderer>().material.color =
            new Color(Random.value, Random.value, Random.value, 1.0f);
        
        // throw the projectile
        Rigidbody newProjectileRigidbody = newProjectile.GetComponent<Rigidbody>();
        newProjectileRigidbody.mass = Mathf.Pow(size, 3);
        newProjectileRigidbody.AddForce(throwPoint.forward * throwObjectForce, ForceMode.Impulse);

        // throw the player
        rb.AddForce(transform.forward * throwPlayerForce, ForceMode.Impulse);
    }

    void OnDash(InputAction.CallbackContext ctx)
    {
        rb.AddForce(transform.forward * dashForce, ForceMode.Impulse);
    }
}
