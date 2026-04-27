using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class CapsulePlayer : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private InputActionAsset inputActions;
    private InputAction lookAction;
    private InputAction takeAction;
    private InputAction throwAction;
    private InputAction resetAction;
    public bool takeThrowSomethingDebug = false;
    public bool takeThrowActionDebug = false;

    [Header("Look Settings")]
    [SerializeField] private Transform playerPivot;
    [SerializeField] private float sensitivity = .5f;
    [SerializeField] private float smoothTime = .5f;
    [SerializeField] private float rotationMaxSpeed = 1000f;
    
    [Header("Fov Settings")]
    [SerializeField] private float minimalFov = 70;
    [SerializeField] private float maximalFov = 140;
    [SerializeField] private float addedFovBySpeed = 2;
    [SerializeField] private float smoothyFovTime = .3f;
    private float smoothyFov = 70;
    private float fovVelocity = 0f;

    [Header("Throw Settings")]
    [SerializeField] private Rigidbody playerBody;
    [SerializeField] private Transform takePoint;
    [SerializeField] private float takeRadius;
    [SerializeField] private Transform throwPoint;
    [SerializeField] private Transform handPoint;
    [SerializeField] private bool cheatProjectileActivated = false;
    [SerializeField] private GameObject cheatProjectilePrefab;
    [SerializeField] private float throwMassBase = 1;
    [SerializeField] private float throwMassInfluence = 1;
    [SerializeField] private float throwObjectForce = 15f;
    [SerializeField] private float throwPlayerForce = -15f;

    [Header("Hand Settings")]
    [SerializeField] private Image handImageUI;
    [SerializeField] private Sprite handSpriteReachable;
    [SerializeField] private Sprite handSpriteIdle;
    [SerializeField] private Sprite handSpriteGrab;


    private Camera cam;
    private Vector3 lastPosition;
    private float rotateEnterCooldown = 1f;
    private Quaternion rotation;
    private Vector3 rotaVelocity = Vector3.zero;
    private Vector3 rotaVelocityVelocity = Vector3.zero;

    // reachable
    private bool anythingReachable = false;
    private GameObject reachableObject = null;

    // in hand
    private bool anythingInHand = false;
    private TakableObject handyTakable = null;
    private GameObject handyObject = null;

    void Awake()
    {
        cam = Camera.main;
        lookAction = inputActions.FindAction("Player/Look");
        takeAction = inputActions.FindAction("Player/Take");
        throwAction = inputActions.FindAction("Player/Throw");
        resetAction = inputActions.FindAction("Player/Reset");
        
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
        resetAction.Enable();

        throwAction.performed += OnThrow;
        takeAction.canceled += OnTake;
        resetAction.performed += OnReset;
    }

    void OnDisable()
    {
        throwAction.performed -= OnThrow;
        takeAction.canceled -= OnTake;
        resetAction.performed -= OnReset;

        lookAction.Disable();
        throwAction.Disable();
        takeAction.Disable();
        resetAction.Disable();
    }

    void Update()
    {
        HandleLook();
        UpdateFov();
        CheckReachable();

        Sprite handSprite;
        if (anythingInHand)
            handSprite = handSpriteGrab;
        else if (reachableObject)
            handSprite = handSpriteReachable;
        else
            handSprite = handSpriteIdle;
        handImageUI.sprite = handSprite;
    }

    void UpdateFov()
    {
        // stop copy me valet :c
        float speed = Vector3.Magnitude(playerBody.linearVelocity);
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

    void CheckReachable()
    {
        if (anythingInHand) return;
        Collider[] inRangeColliders = Physics.OverlapSphere(takePoint.position, takeRadius);
        Vector3 centerPoint = takePoint.position;// could be transform.position
        GameObject nearestObject = null;
        float nearestDistance = 100;
        foreach (var loopCollider in inRangeColliders)
        {
            GameObject loopObject = loopCollider.gameObject;
			if (!loopObject.TryGetComponent(out TakableReference loopTakeRef)) continue;
			float loopDistance = Vector3.Distance(centerPoint, loopCollider.transform.position);
            if (!(loopDistance < nearestDistance)) continue;
            nearestObject = loopCollider.gameObject;
            nearestDistance = loopDistance;
        }
        reachableObject = nearestObject;
        anythingReachable = nearestObject != null;
    }

    void OnTake(InputAction.CallbackContext ctx)
    {
        if (takeThrowActionDebug) Debug.Log("player take action");
        CheckReachable();// recheck reachability to avoid null exception
        if (anythingInHand || !anythingReachable) return;
        if (takeThrowSomethingDebug) Debug.Log("player take something");
        TookObject(reachableObject);
    }

    void OnThrow(InputAction.CallbackContext ctx)
    {
        if (takeThrowActionDebug) Debug.Log("player throw action");
        if (!anythingInHand)
        {
            if (cheatProjectileActivated)
            {
                GameObject projectile = Instantiate(cheatProjectilePrefab, throwPoint.position, Quaternion.identity);
                projectile.GetComponent<MeshRenderer>().material.color = new Color(Random.value, Random.value, Random.value, 1.0f);
                ThrowObject(projectile);
            }
            return;
        }
        if (takeThrowSomethingDebug) Debug.Log("player throw something");
        ThrowObject(handyObject);
    }

    void TookObject(GameObject takeObject)
    {
        TakableObject takable = takeObject.GetComponent<TakableReference>().takableObject;
        takable.InHand(handPoint);
        handyTakable = takable;
        handyObject = takeObject;
        anythingInHand = true;
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
            anythingInHand = false;
        } 
        
        // throw the projectile
        throwBody.AddForce(throwCommonForce * throwObjectForce * throwPoint.forward, ForceMode.Impulse);

        // throw the player
        playerBody.AddForce(throwCommonForce * throwPlayerForce * transform.forward, ForceMode.Impulse);

    }

    void OnReset(InputAction.CallbackContext ctx)
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

	void OnDrawGizmos()
	{
        Gizmos.color = Color.azure;
        if (anythingInHand) Gizmos.color = Color.skyBlue;
        else if (anythingReachable) Gizmos.color = Color.green;
        else Gizmos.color = Color.red;
        Gizmos.color = new Color(Gizmos.color.r, Gizmos.color.g, Gizmos.color.b, 0.3f);
		Gizmos.DrawSphere(takePoint.position, takeRadius);
	}
}
