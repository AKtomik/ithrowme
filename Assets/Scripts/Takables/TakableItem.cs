using UnityEngine;
using UnityEngine.Audio;

public class TakableItem : Takable
{
    [Header("Item Pointers")]
    [SerializeField] protected Rigidbody rigidBody;
    private Transform originalParentTransform;
    private int originalLayer;

    [Header("Item Sounds")]
    public bool disableAudio = false;
    public AudioClip[] hitAudio = new AudioClip[] { null };
    public AudioClip[] takeAudio = new AudioClip[] { null };
    public AudioClip[] throwAudio = new AudioClip[] { null };
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Start()
    {
        originalParentTransform = transform.parent;
        if (disableAudio) Debug.LogWarning("item with disabled audio: "+this);
        base.Start();
    }

    virtual public void Put(Transform pointParent)
    {
        // stop
        rigidBody.linearVelocity = Vector3.zero;
        rigidBody.angularVelocity = Vector3.zero;
        // disable
        takeCollider.enabled = false;
        rigidBody.isKinematic = true;
        // reparent
        transform.SetParent(pointParent);
        transform.position = pointParent.position;
        // reset pos
        if (takeCollider.gameObject != gameObject)
            takeCollider.gameObject.transform.position = pointParent.position;
        // play take sound
        PlaySound(takeAudio);

        originalLayer = takeCollider.gameObject.layer;
        takeCollider.gameObject.layer = LayerMask.NameToLayer("CullingLayer");
    }
    
    virtual public void Unput(Transform point)
    {
        // enable
        takeCollider.enabled = true;
        rigidBody.isKinematic = false;
        // reparent
        transform.SetParent(originalParentTransform);
        // move the projectile
        transform.SetPositionAndRotation(point.position, point.rotation);
        // play throw sound
        PlaySound(throwAudio);

        takeCollider.gameObject.layer = originalLayer;
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
        // remove from hand
        player.ClearHand();
    }

    // audio
    public void PlaySound(AudioClip[] audioClips, float audioVolume = 1f, float pitch = 1f)
    {
        PlaySound(audioClips[Random.Range(0, audioClips.Length)], audioVolume, pitch);
    }

    public void PlaySound(AudioClip audioClip, float audioVolume = 1f, float pitch = 1f)
    {
        if (disableAudio) return;
        audioSource.pitch = pitch;
        audioSource.volume = audioVolume;
        audioSource.PlayOneShot(audioClip);
    }

    private void OnCollisionEnter(Collision collision)
    {
        PlaySound(hitAudio, .3f, Random.Range(0.7f, 1.5f));
    }
}
