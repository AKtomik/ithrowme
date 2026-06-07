using System.Collections.Generic;
using UnityEditor;
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
    private InputAction rollAction;
    private InputAction middleAction;
    private InputAction takeAction;
    private InputAction throwAction;
    private InputAction resetAction;
    public bool takeThrowSomethingDebug = false;
    public bool takeThrowActionDebug = false;

    [Header("Look Settings")]
    [SerializeField] private Transform playerPivot;
    [SerializeField] private CanvasBoss canvasMana;
    [SerializeField] private float lookSensitivity = .5f;
    [SerializeField] private float smoothTime = .5f;
    [SerializeField] private float rotationMaxSpeed = 10000000000f;
    [SerializeField] private float rollSensitivity = 2f;
    
    [Header("Fov Settings")]
    [SerializeField] private bool usingSettingsFov = true;
    [SerializeField, DrawIf("usingSettingsFov", false)] private float manualMinimalFov = 60;
    [SerializeField, DrawIf("usingSettingsFov", false)] private float manualMaximalFov = 100;
    [SerializeField, DrawIf("usingSettingsFov", true)] private float baseRangeToMaxFov = 40;
    [SerializeField] private float addedFovBySpeed = 2;
    [SerializeField] private float smoothyFovTime = .3f;
    private float smoothyFov = 70;
    private float fovVelocity = 0f;

    [Header("Throw Settings")]
    [SerializeField] public Rigidbody playerBody;
    [SerializeField] public Transform takePoint;
    [SerializeField] public float takeRadius;
    [SerializeField] public Transform throwPoint;
    [SerializeField] public Transform handPoint;
    [SerializeField] private LayerMask reachableMask;
    [SerializeField] private bool cheatProjectileActivated = false;
    [SerializeField] private GameObject cheatProjectilePrefab;
    [SerializeField] public float throwMassBase = 1;
    [SerializeField] public float throwMassInfluence = 1;
    [SerializeField] public float throwObjectForce = 15f;
    [SerializeField] public float throwPlayerForce = -15f;


    [Header("Lock Utils")]
    public bool disableHand = false;
    public bool disableLook = false;
    private bool lockLookAt = false;
    private Vector3 lockLookAtPos = Vector3.zero;
    private float lockLookAtSpeed = 1;
    private float lockLookAtProgress = 0;
    
    [Header("Sound")]
    [SerializeField] public bool disableAudio = false;
    [SerializeField] private AudioClip[] lightHitAudio = new AudioClip[]{ null };
    [SerializeField] private AudioClip[] strongHitAudio = new AudioClip[]{ null };
    [SerializeField] private AudioClip[] breathingAudio = new AudioClip[]{ null };
    [SerializeField] private AudioClip[] takingDamageAudio = new AudioClip[]{ null };
    [SerializeField] private AudioSource feedbackAudioSource;
    [SerializeField] private AudioSource breathAudioSource;


    private Camera cam;
    
    private Vector3 lastPosition;
    private float rotateEnterCooldown = 1f;
    private Quaternion rotation;
    private Vector3 rotaVelocity = Vector3.zero;
    private Vector3 rotaVelocityVelocity = Vector3.zero;


    // reachable
    private bool anythingReachable = false;
    public GameObject reachableObject = null;
    private List<TakableReference> takeNoRepeatList = new List<TakableReference>();

    // in hand
    public bool anythingInHand = false;
    private Takable handyTakable = null;
    private GameObject handyObject = null;
    private int throwCount = 0;

    void Awake()
    {
        cam = Camera.main;

        lookAction = inputActions.FindAction("Player/Look");
        rollAction = inputActions.FindAction("Player/Roll");
        middleAction = inputActions.FindAction("Player/Middle");
        takeAction = inputActions.FindAction("Player/Take");
        throwAction = inputActions.FindAction("Player/Throw");
        resetAction = inputActions.FindAction("Player/Reset");
        
        rotation = playerPivot.rotation;
        lastPosition = transform.position;
    }

    void OnEnable()
    {
        lookAction.Enable();
        rollAction.Enable();
        middleAction.Enable();
        throwAction.Enable();
        takeAction.Enable();
        resetAction.Enable();

        throwAction.performed += OnThrow;
        takeAction.performed += OnTake;
        resetAction.performed += OnReset;
    }

    void OnDisable()
    {
        throwAction.performed -= OnThrow;
        takeAction.performed -= OnTake;
        resetAction.performed -= OnReset;

        lookAction.Disable();
        rollAction.Disable();
        middleAction.Disable();
        throwAction.Disable();
        takeAction.Disable();
        resetAction.Disable();
    }

    void Update()
    {
        if (Time.timeScale == 0) return;
        if (!disableLook) HandleLook();
        UpdateFov();
        CheckTimer();
        CheckReachable();
        lastPosition = transform.position;
    }

    void CheckTimer()
    {
        // will spam play when moving, until the end
        if (transform.position != lastPosition) TimerScript.instance.PlayTime();
    }

    private void OnCollisionEnter(Collision collision)
    {
        

        if (!(collision.gameObject.CompareTag("Items")) 
            && collision.GetContact(0).thisCollider is SphereCollider
            && rotaVelocity.z < 0.3)
        {
            
            if (playerBody.linearVelocity.magnitude > 3.5f)
            {
                PlaySound(strongHitAudio, Random.Range(.7f, .9f), Random.Range(1f, 1.5f));
                //PlaySound(takingDamageAudio, Random.Range(0.7f, 0.9f), Random.Range(1f, 1.5f)); // hurt sound
                
            }
            else if (playerBody.linearVelocity.magnitude > 0.8f)
            {
                PlaySound(lightHitAudio, Random.Range(.7f, 1f), Random.Range(.8f, 1.2f));
            }
        }

    }


    void UpdateFov()
    {
        // stop copy me valet :c
        float speed = Vector3.Magnitude(playerBody.linearVelocity);
        float minFov;
        float maxFov;
        if (usingSettingsFov) {
            minFov = SettingsStore.baseFov;
            maxFov = SettingsStore.baseFov + baseRangeToMaxFov;
        } else {
            minFov = manualMinimalFov;
            maxFov = manualMaximalFov;
        }
        float claculatedFov = minFov + addedFovBySpeed * speed;
        if (claculatedFov > maxFov) claculatedFov = maxFov;
        smoothyFov = Mathf.SmoothDamp(smoothyFov, claculatedFov, ref fovVelocity, smoothyFovTime);
        cam.fieldOfView = smoothyFov;
    }

    void HandleLook()
    {
        if (lockLookAt)
        {
            if (lockLookAtPos != Vector3.zero)
            {
                // toward from here to point
                Vector3 towardLook = lockLookAtPos - playerPivot.position;
                towardLook = towardLook.normalized;

                // t compute
                lockLookAtProgress += lockLookAtSpeed * Time.deltaTime;
                if (lockLookAtProgress > 1) lockLookAtProgress = 1;
                
                // look toward
                Quaternion noRollToward = Quaternion.LookRotation(towardLook);
                Quaternion noRollCurrent = Quaternion.LookRotation(rotation * Vector3.forward);
                Quaternion deltaRoll = Quaternion.Inverse(noRollCurrent) * rotation;
                Quaternion toward = noRollToward * deltaRoll;// preserve z roll

                // progressive move
                rotation = Quaternion.Slerp(rotation, toward, lockLookAtProgress);
                playerPivot.rotation = rotation;
            }
            return;// stop player look control
        }

        Vector2 mouseInput = lookAction.ReadValue<Vector2>();
        float rollInput = rollAction.ReadValue<float>();
        Vector3 rotaInput = Vector3.zero;
        if (mouseInput.magnitude > 0.01)
        {
            if (rotateEnterCooldown > 0)
            {
                rotateEnterCooldown -= 1;
            } else if (middleAction.IsPressed()) {
                rotaInput.z += mouseInput.x;
            } else {
                rotaInput.x -= mouseInput.x;
                rotaInput.y += mouseInput.y;
            }
        }
        
        //if (mouseInput.magnitude > 0.01)
        if (SettingsStore.invertRoll)
        {
            rotaInput.z += rollInput * -1 * rollSensitivity * SettingsStore.rollSensivity;
        }
        else
        {
            rotaInput.z -= rollInput * -1 * rollSensitivity * SettingsStore.rollSensivity;
        }
        

        rotaVelocity += lookSensitivity * SettingsStore.lookSensivity * rotaInput;
        rotaVelocity = Vector3.SmoothDamp(rotaVelocity, Vector3.zero, ref rotaVelocityVelocity, smoothTime, rotationMaxSpeed, Time.deltaTime);
        Vector3 rotaDelta = -rotaVelocity * Time.deltaTime;
        
        if (rotaDelta.magnitude < 0.001f) return;
        Quaternion angleX = Quaternion.AngleAxis(rotaDelta.x, rotation * Vector3.up);
        rotation = angleX * rotation;
        Quaternion angleY = Quaternion.AngleAxis(rotaDelta.y, rotation * Vector3.right);
        rotation = angleY * rotation;
        Quaternion angleZ = Quaternion.AngleAxis(rotaDelta.z, rotation * Vector3.forward);
        rotation = angleZ * rotation;
        playerPivot.rotation = rotation;
    }

    public void LockingLookAt()
    {
        canvasMana.EnableCinematic();
        lockLookAt = true;
        lockLookAtPos = Vector2.zero;
        lockLookAtSpeed = 0;
        lockLookAtProgress = 0;

        TimerScript.instance.PauseTime();
    }

    public void LockingLookAt(Vector3 pos, float speed = 1)
    {
        canvasMana.EnableCinematic();
        lockLookAt = true;
        lockLookAtPos = pos;
        lockLookAtSpeed = speed;
        lockLookAtProgress = 0;
        
        TimerScript.instance.PauseTime();
    }
    
    public void UnlockingLook()
    {
        canvasMana.DisableCinematic();
        lockLookAt = false;
        
        TimerScript.instance.PlayTime();
    }

    void CheckReachable()
    {
        if (anythingInHand) return;
        Collider[] inRangeColliders = Physics.OverlapSphere(takePoint.position, takeRadius, reachableMask);
        List<TakableReference> newNoRepeatList = new List<TakableReference>();
        Vector3 centerPoint = takePoint.position;// could be transform.position
        GameObject nearestObject = null;
        float nearestDistance = 100;
        foreach (var loopCollider in inRangeColliders)
        {
            GameObject loopObject = loopCollider.gameObject;
			if (!loopObject.TryGetComponent(out TakableReference loopTakeRef)) continue;
			float loopDistance = Vector3.Distance(centerPoint, loopCollider.transform.position);
            if (takeNoRepeatList.Contains(loopTakeRef))
            {
                newNoRepeatList.Add(loopTakeRef);
                continue;
            }
            if (!(loopDistance < nearestDistance)) continue;
            nearestObject = loopCollider.gameObject;
            nearestDistance = loopDistance;
        }
        takeNoRepeatList = newNoRepeatList;
        reachableObject = nearestObject;
        anythingReachable = nearestObject != null;
    }

    void OnTake(InputAction.CallbackContext ctx)
    {
        if (Time.timeScale == 0 || disableHand) return;
        if (takeThrowActionDebug) Debug.Log("player take action");
        CheckReachable();// recheck reachability to avoid null exception
        if (anythingInHand || !anythingReachable) return;
        if (takeThrowSomethingDebug) Debug.Log("player take something");
        TookSomething(reachableObject);
    }

    void OnThrow(InputAction.CallbackContext ctx)
    {
        if (Time.timeScale == 0) return;
        if (takeThrowActionDebug) Debug.Log("player throw action");
        if (!anythingInHand)
        {
            if (cheatProjectileActivated)
            {
                GameObject projectile = Instantiate(cheatProjectilePrefab, throwPoint.position, Quaternion.identity);
                projectile.GetComponent<MeshRenderer>().material.color = new Color(Random.value, Random.value, Random.value, 1.0f);
                TakableItem takable = projectile.GetComponent<TakableItem>();
                //takable.Start();
                takable.Throw(this);
            }
            return;
        }
        if (takeThrowSomethingDebug) Debug.Log("player throw something");
        handyTakable.Throw(this);
    }

    void TookSomething(GameObject takeObject)
    {
        TakableReference takableRef = takeObject.GetComponent<TakableReference>();
        Takable takable = takableRef.takable;
        takeNoRepeatList.Add(takableRef);
        takable.Take(this);
    }

    public void PutInHand(Takable takable)
    {
        handyTakable = takable;
        handyObject = takable.gameObject;
        anythingInHand = true;
    }
    
    public void ClearHand()
    {
        handyTakable = null;
        handyObject = null;
        anythingInHand = false;
        // cool to count
        throwCount++;
    }
    
    // reset input
    void OnReset(InputAction.CallbackContext ctx)
    {
        if (Time.timeScale == 0) return;
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    // audio
    public void PlaySound(AudioClip[] audioClips, float audioVolume = 1f, float pitch = 1f, bool breath = false)
    {
        PlaySound(audioClips[Random.Range(0, audioClips.Length)], audioVolume, pitch, breath);
    }

    public void PlaySound(AudioClip audioClip, float audioVolume = 1f, float pitch = 1f, bool breath = false)
    {
        if (disableAudio) return;
        AudioSource audioSource = (breath) ? breathAudioSource : feedbackAudioSource;
        audioSource.pitch = pitch;
        audioSource.volume = audioVolume;
        audioSource.PlayOneShot(audioClip);
    }

    // gizmos
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
