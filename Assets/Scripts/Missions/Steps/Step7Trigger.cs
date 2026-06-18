using UnityEngine;

// credit trigger
public class Step7Trigger : MonoBehaviour
{
    [Header("Step Pointers")]
    [SerializeField] private MissionManager missionManager;
    [SerializeField] private Transform placingPoint;
    [SerializeField] private Transform lookingPoint;
    [SerializeField] private Animation pulledAnimatorReference;

    private bool entered = false;
    private CapsulePlayer finishPlayer;

    private void OnTriggerEnter(Collider other) {
        if (entered || !other.gameObject.CompareTag("Player")) return;
        if (!other.gameObject.TryGetComponent<CapsulePlayer>(out var player)) return;
        Debug.Log("Step0Trigger: step 7 completed");
        entered = true;
        finishPlayer = player;
        
        // mission
        missionManager.CompleteMission(7);
        //missionManager.AddMission(8);
        TimerSingleton.instance.EndTime();// ! this is the end, gg
        
        // player
        player.playerBody.isKinematic = true;
        ReferenceSingleton.instance.cinematicManager.EnableCinematic();
        
        player.transform.SetParent(placingPoint);
        player.transform.localPosition = Vector3.zero;
        
        player.LockingLookAt(lookingPoint);

        // animation
        pulledAnimatorReference.Play();
    }

    public void AnimationEnded() {
        ReferenceSingleton.instance.cinematicManager.DisableCinematic();
        finishPlayer.UnlockingLook();
    }
}
