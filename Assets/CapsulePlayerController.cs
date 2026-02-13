using UnityEngine;
using UnityEngine.InputSystem;

public class CapsulePlayerController : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private InputActionAsset inputActions;
    private InputAction lookAction;
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
    private Vector2 rotationRaw;
    private Vector2 rotationSmooth;
    private Vector2 rotationVelocity = new(0, 0);

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        lookAction = inputActions.FindAction("Player/Look");
        throwAction = inputActions.FindAction("Player/Throw");
        dashAction = inputActions.FindAction("Player/Dash");
    }

    void OnEnable()
    {
        lookAction.Enable();
        throwAction.Enable();
        dashAction.Enable();

        throwAction.performed += OnThrow;
        dashAction.performed += OnDash;
    }

    void OnDisable()
    {
        throwAction.performed -= OnThrow;
        dashAction.performed -= OnDash;

        lookAction.Disable();
        throwAction.Disable();
        dashAction.Disable();
    }

    void Update()
    {
        HandleLook();
    }

    void HandleLook()
    {
        Vector2 input = lookAction.ReadValue<Vector2>() * sensitivity;
        
        input.y *= -1;
        rotationRaw += input;
        rotationSmooth = Vector2.SmoothDamp(rotationSmooth, rotationRaw, ref rotationVelocity, smoothTime, rotationMaxSpeed, Time.deltaTime);
        //rotationSmooth.x = Mathf.SmoothDamp(rotationSmooth.x, rotationRaw.x, ref rotationVelocity.x, smoothTime);
        //rotationSmooth.y = Mathf.SmoothDamp(rotationSmooth.y, rotationRaw.y, ref rotationVelocity.y, smoothTime);
        Debug.Log("rotationRaw:"+ rotationRaw + "rotationSmooth:"+ rotationSmooth);
        //rotationSmooth = Vector2.MoveTowards(rotationSmooth, rotationRaw, rotationMaxSpeed * Time.deltaTime);
        playerPivot.localRotation = Quaternion.Euler(rotationSmooth.y, rotationSmooth.x, 0f);
    }

    void OnThrow(InputAction.CallbackContext ctx)
    {
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
