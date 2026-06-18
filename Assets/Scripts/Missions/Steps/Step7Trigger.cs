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

    private void OnTriggerEnter(Collider other) {
        if (entered || !other.gameObject.CompareTag("Player")) return;
        if (!other.gameObject.TryGetComponent<CapsulePlayer>(out var player)) return;
        Debug.Log("Step0Trigger: step 7 completed");
        entered = true;
        
        // mission
        missionManager.CompleteMission(7);
        missionManager.AddMission(8);
        
        // player
        //ReferenceSingleton.instance.cinematicManager.EnableCinematic();
        player.playerBody.isKinematic = true;
        player.transform.SetParent(placingPoint);
        player.transform.localPosition = Vector3.zero;
        
        player.LockingLookAt(lookingPoint);

        // animation
        pulledAnimatorReference.Play();
    }
}
