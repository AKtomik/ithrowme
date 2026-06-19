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
        //if (!missionManager.IsActive(7)) return;
        Debug.Log("Step0Trigger: step 7 completed");
        entered = true;
        finishPlayer = player;
        
        // mission
        missionManager.CompleteMission(7);
        //missionManager.AddMission(8);
        TimerSingleton.instance.EndTime();// ! this is the end, gg
        
        // player
        ReferenceSingleton.instance.cinematicManager.EnableCinematic(false, false, true);
        player.transform.SetParent(placingPoint);
        player.LockingLookAt(lookingPoint);
        player.LockingPosAt(placingPoint);

        // animation
        pulledAnimatorReference.Play();
    }

    // ! is not executed by the same Step7Trigger than the trigger
    public void AnimationEnded() {
        ReferenceSingleton.instance.cinematicManager.DisableCinematic();
        ReferenceSingleton.instance.player.UnlockingLook();
        ReferenceSingleton.instance.player.UnlockingPos();
    }
}
