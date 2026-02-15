using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
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
    private Quaternion rotation;
    private Vector3 rotaVelocity = Vector3.zero;
    private Vector3 rotaVelocityVelocity = Vector3.zero;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        lookAction = inputActions.FindAction("Player/Look");
        takeAction = inputActions.FindAction("Player/Take");
        throwAction = inputActions.FindAction("Player/Throw");
        dashAction = inputActions.FindAction("Player/Dash");
        
        rotation = playerPivot.localRotation;
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
        Vector2 mouseInput = lookAction.ReadValue<Vector2>();
        Vector3 rotaInput = Vector3.zero;
        if (mouseInput.magnitude > 0.01)
        {
            if (rotateEnterCooldown > 0)
            {
                rotateEnterCooldown -= 1;
            }
            else if (takeAction.IsPressed())
            {
                rotaInput.z += mouseInput.x;
            } else {
                rotaInput.x -= mouseInput.x;
                rotaInput.y += mouseInput.y;
            }
        }
        rotaVelocity += sensitivity * rotaInput;
        rotaVelocity = Vector3.SmoothDamp(rotaVelocity, Vector3.zero, ref rotaVelocityVelocity, smoothTime, rotationMaxSpeed, Time.deltaTime);
        Vector3 rotaDelta = -rotaVelocity * Time.deltaTime;
        
        if (rotaDelta.magnitude < 0.001f) return;
        Quaternion angleX = Quaternion.AngleAxis(rotaDelta.x, rotation * Vector3.up);
        rotation = angleX * rotation;
        Quaternion angleY = Quaternion.AngleAxis(rotaDelta.y, rotation * Vector3.right);
        rotation = angleY * rotation;
        Quaternion angleZ = Quaternion.AngleAxis(rotaDelta.z, rotation * Vector3.forward);
        rotation = angleZ * rotation;
        playerPivot.localRotation = rotation;
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
