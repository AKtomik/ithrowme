using UnityEngine;
using UnityEngine.Audio;

public class TakableItem : Takable
{
    [Header("Item Pointers")]
    [SerializeField] protected Rigidbody rigidBody;
    private Transform originalParentTransform;

    [Header("Item Sounds")]
    public bool disableAudio = false;
    public AudioClip[] soundList; // 0 = hitAudio, 1 = takeAudio, 2 = throwAudio
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Start()
    {
        originalParentTransform = transform.parent;
        if (disableAudio) Debug.LogWarning("item with disabled audio: "+this);

        base.Start();
    }

    public void Put(Transform parent)
    {
        // stop
        rigidBody.linearVelocity = Vector3.zero;
        rigidBody.angularVelocity = Vector3.zero;
        // disable
        takeCollider.enabled = false;
        rigidBody.isKinematic = true;
        // reparent
        transform.SetParent(parent);
        transform.position = parent.position;
        // reset pos
        if (takeCollider.gameObject != gameObject)
            takeCollider.gameObject.transform.position = parent.position;
        // play take sound
        if (disableAudio) return;
        audioSource.PlayOneShot(soundList[1]);
    }
    
    public void Unput(Transform point)
    {
        // enable
        takeCollider.enabled = true;
        rigidBody.isKinematic = false;
        // reparent
        transform.SetParent(originalParentTransform);
        // move the projectile
        transform.SetPositionAndRotation(point.position, point.rotation);
    }

    override public void Take(CapsulePlayer player)
    {
        Put(player.handPoint);
        player.PutInHand(this);
    }
    
    override public void Throw(CapsulePlayer player)
    {
        Unput(player.throwPoint);

        // throw
        float throwCommonForce = player.throwMassBase + rigidBody.mass * player.throwMassInfluence;
        // throw the projectile
        rigidBody.AddForce(throwCommonForce * player.throwObjectForce * player.throwPoint.forward, ForceMode.Impulse);
        // throw the player
        player.playerBody.AddForce(throwCommonForce * player.throwPlayerForce * transform.forward, ForceMode.Impulse);

        // play throw sound
        if (!disableAudio) {
            audioSource.PlayOneShot(soundList[2]);
        }

        // remove from hand
        player.ClearHand();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (disableAudio) return;
        audioSource.pitch = Random.Range(0.7f, 1.5f);
        audioSource.volume = 0.3f;
        audioSource.PlayOneShot(soundList[0]);
    }
}
