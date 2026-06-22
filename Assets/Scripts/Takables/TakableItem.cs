using UnityEngine;
using UnityEngine.Audio;

public class TakableItem : Takable
{
    [Header("Item Pointers")]
    [SerializeField] protected Rigidbody rigidBody;
    [SerializeField] protected GameObject affiliated;
    
    private int originalLayer = -1;
    private Transform originalParentTransform;
    private Transform putParentTransform;
    private MovingThing movingThing;

    [Header("Item Sounds")]
    public bool disableAudio = false;
    public AudioClip[] hitAudio = new AudioClip[] { null };
    public AudioClip[] takeAudio = new AudioClip[] { null };
    public AudioClip[] throwAudio = new AudioClip[] { null };
    
    private bool isPut;
    public bool IsPut { get => isPut; }
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Start()
    {
        
        if (disableAudio) Debug.LogWarning("item with disabled audio: "+this);
        base.Start();
    }

    virtual public void Put(Transform pointParent, bool doCulling = true)
    {
        // stop
        rigidBody.linearVelocity = Vector3.zero;
        rigidBody.angularVelocity = Vector3.zero;
        // disable
        isPut = true;
        takeCollider.enabled = false;
        if (movingThing)
            movingThing.SetKinematic(true);
        else
            rigidBody.isKinematic = true;
        // reparent
        originalParentTransform = transform.parent;
        putParentTransform = pointParent;
        transform.SetParent(pointParent);
        transform.position = pointParent.position;
        // relayer
        if (doCulling)
        {
            originalLayer = takeCollider.gameObject.layer;
            takeCollider.gameObject.layer = LayerMask.NameToLayer("CullingLayer");
            if (affiliated) SetLayerRecursively(affiliated, LayerMask.NameToLayer("CullingLayer"));
            gameObject.layer = LayerMask.NameToLayer("CullingLayer");
        }
        // reset pos
        if (takeCollider.gameObject != gameObject)
            takeCollider.gameObject.transform.position = pointParent.position;
        // play take sound
        PlaySound(takeAudio);
    }
    
    virtual public void Unput(Transform point)
    {
        // enable
        isPut = false;
        takeCollider.enabled = true;
        rigidBody.isKinematic = false;
        // reparent
        if (transform.parent != putParentTransform) Debug.LogError("item was reparented while puted");
        transform.SetParent(originalParentTransform);
        // relayer
        if (originalLayer != -1)
        {
            takeCollider.gameObject.layer = originalLayer;
            if (affiliated) SetLayerRecursively(affiliated, originalLayer);
            gameObject.layer = originalLayer;
            originalLayer = -1;
        }
        // move the projectile
        transform.SetPositionAndRotation(point.position, point.rotation);
        // play throw sound
        PlaySound(throwAudio);
    }
    
    void SetLayerRecursively(GameObject obj, int layer)
    {
        obj.layer = layer;
        foreach (Transform child in obj.transform)
            SetLayerRecursively(child.gameObject, layer);
    }

    public void Reparent(Transform parnt)
    {
        if (transform.parent == parnt) return;
        if (isPut) originalParentTransform = parnt;
        else transform.SetParent(parnt);
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
