using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class CapsulePlayer : MonoBehaviour
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
    [SerializeField] private bool cheatProjectileActivated = false;
    [SerializeField] private GameObject cheatProjectilePrefab;
    [SerializeField] private Transform throwPoint;
    [SerializeField] private Transform takePoint;
    [SerializeField] private Transform handPoint;
    [SerializeField] private float throwMassBase = 1;
    [SerializeField] private float throwMassInfluence = 1;
    [SerializeField] private float throwObjectForce = 15f;
    [SerializeField] private float throwPlayerForce = -15f;

    [Header("Camera Settings")]
    [SerializeField] private float minimalFov = 70;
    [SerializeField] private float maximalFov = 140;
    [SerializeField] private float addedFovBySpeed = 2;
    [SerializeField] private float smoothyFovTime = .3f;
    private float smoothyFov = 70;
    private float fovVelocity = 0f;

    [Header("Dash Settings")]
    [SerializeField] private float dashForce = 20f;

    private Rigidbody rb;
    private Camera cam;
    private Vector3 lastPosition;
    private float rotateEnterCooldown = 1f;
    private Quaternion rotation;
    private Vector3 rotaVelocity = Vector3.zero;
    private Vector3 rotaVelocityVelocity = Vector3.zero;

    // in hand
    private TakableObject handyTakable = null;
    private GameObject handyObject = null;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;
        lookAction = inputActions.FindAction("Player/Look");
        takeAction = inputActions.FindAction("Player/Take");
        throwAction = inputActions.FindAction("Player/Throw");
        dashAction = inputActions.FindAction("Player/Dash");
        
        rotation = playerPivot.localRotation;
        lastPosition = transform.position;
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void OnEnable()
    {
        lookAction.Enable();
        throwAction.Enable();
        takeAction.Enable();
        dashAction.Enable();

        throwAction.performed += OnThrow;
        takeAction.canceled += OnTake;
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
        UpdateFov();
    }

    void UpdateFov()
    {
        // stop copy me valet :c
        float speed = Vector3.Magnitude(rb.linearVelocity);
        lastPosition = transform.position;
        float claculatedFov = minimalFov + addedFovBySpeed * speed;
        if (claculatedFov > maximalFov) claculatedFov = maximalFov;
        smoothyFov = Mathf.SmoothDamp(smoothyFov, claculatedFov, ref fovVelocity, smoothyFovTime);
        cam.fieldOfView = smoothyFov;
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
        if (handyObject != null) return;
        Collider[] hitColliders = Physics.OverlapSphere(takePoint.position, 1);
        GameObject takeObject = null;
        TakableObject takeTake = null;
        float takeDistance = 100;
        foreach (var hitCollider in hitColliders)
        {
            TakableObject hitTake = null;
            if (!hitCollider.TryGetComponent(out hitTake)) continue;
            float hitDistance = Vector3.Distance(transform.position, hitCollider.transform.position);
            if (!(hitDistance < takeDistance)) continue;
            takeObject = hitCollider.gameObject;
            takeDistance = hitDistance;
            takeTake = hitTake;
        }
        if (takeTake == null) return;
        TookObject(takeObject, takeTake);
    }

    void OnThrow(InputAction.CallbackContext ctx)
    {
        Debug.Log("player throw action");
        if (handyObject == null)
        {
            if (cheatProjectileActivated)
            {
                GameObject projectile = Instantiate(cheatProjectilePrefab, throwPoint.position, Quaternion.identity);
                projectile.GetComponent<MeshRenderer>().material.color = new Color(Random.value, Random.value, Random.value, 1.0f);
                ThrowObject(projectile);
            }
            return;
        }
        ThrowObject(handyObject);
    }

    void TookObject(GameObject takeObject, TakableObject takable)
    {
        takable.InHand(handPoint);
        handyTakable = takable;
        handyObject = takeObject;
    }
    
    void ThrowObject(GameObject throwObject)
    {
        Rigidbody throwBody = throwObject.GetComponent<Rigidbody>();
        float throwCommonForce = throwMassBase + throwBody.mass * throwMassInfluence;

        // move the projectile
        throwObject.transform.SetPositionAndRotation(throwPoint.position, throwPoint.rotation);
        
        // clear hand
        if (handyTakable != null)
        {
            handyTakable.OffHand();
            handyTakable = null;
            handyObject = null;
        } 
        
        // throw the projectile
        throwBody.AddForce(throwCommonForce * throwObjectForce * throwPoint.forward, ForceMode.Impulse);

        // throw the player
        rb.AddForce(throwCommonForce * throwPlayerForce * transform.forward, ForceMode.Impulse);

    }

    void OnDash(InputAction.CallbackContext ctx)
    {
        rb.AddForce(transform.forward * dashForce, ForceMode.Impulse);
    }
}
