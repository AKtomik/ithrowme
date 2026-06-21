using TMPro;
using UnityEngine;

// credit trigger
public class Step7Trigger : MonoBehaviour
{
    [SerializeField] private AudioSource ejectionAudio;
    [SerializeField] private EndCreditScript creditScript;
    [Header("Step Pointers")]
    [SerializeField] private MissionManager missionManager;
    [SerializeField] private Transform placingPoint;
    [SerializeField] private Transform lookingWindowPoint;
    [SerializeField] private Transform lookingScreenPoint;
    [SerializeField] private Animation pulledAnimatorReference;
    
    [SerializeField] private ScrollParallaxGroup parallaxGroup;
    [SerializeField] private TextMeshProUGUI screenTextMesh;
    [SerializeField] private TextMeshProUGUI timeTextMesh;
    [SerializeField] private PauseManager pauseManager;

    private bool entered = false;
    private CapsulePlayer pullPlayer;

	void Start()
	{
		screenTextMesh.enabled = false;
        timeTextMesh.enabled = false;
	}

	private void OnTriggerEnter(Collider other) {
        if (entered || !other.gameObject.CompareTag("Player")) return;
        if (!other.gameObject.TryGetComponent<CapsulePlayer>(out var player)) return;
        //if (!missionManager.IsActive(7)) return;
        Debug.Log("Step0Trigger: step 7 completed");
        entered = true;
        pullPlayer = player;
        
        // mission
        missionManager.CompleteMission(7);
        //missionManager.AddMission(8);
        
        // player
        player.transform.SetParent(placingPoint);
        player.LockingLookAt(lookingWindowPoint, .5f);
        player.LockingPosAt(placingPoint, .5f);

        pauseManager.blockPauseInCinematic = true;

        // animation
        pulledAnimatorReference.Play();
        ejectionAudio.Play();
        
        // text
        screenTextMesh.text = "READY";
        screenTextMesh.enabled = true;
        
        ReferenceSingleton.instance.cinematicManager.EnableCinematic(false, false, true);
        ReferenceSingleton.instance.bothCanvas.ChangeCinematicHandState(HandState.GRAB);
    }

    public void AnimationLookScreen()
    {
        pullPlayer.LockingLookAt(lookingScreenPoint, .2f);
    }

    public void AnimationCountDown3() {
        screenTextMesh.text = "3";
    }
    
    public void AnimationCountDown2() {
        screenTextMesh.text = "2";
    }
    
    public void AnimationCountDown1() {
        screenTextMesh.text = "1";
    }
    
    public void AnimationCountDown0() {
        screenTextMesh.text = "LAUNCH";
        
        TimerSingleton.instance.EndTime();// ! this is the end, gg
        
        timeTextMesh.enabled = true;
        timeTextMesh.text = TimerSingleton.instance.StringTime;
    }
    
    public void AnimationLookWindow() {
        pullPlayer.LockingLookAt(lookingWindowPoint, .025f);
    }
    
    public void AnimationLaunch() {
    }

    public void AnimationEnableScroll()
    {
        parallaxGroup.EnableScrolling();
    }

    public void AnimationEnded() {
        ReferenceSingleton.instance.cinematicManager.DisableCinematic();
        ReferenceSingleton.instance.player.UnlockingLook();
        ReferenceSingleton.instance.player.UnlockingPos();
        creditScript.StartEnding();
    }
}
