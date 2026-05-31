using UnityEngine;

public class TakableItem : Takable
{
    [SerializeField] protected Rigidbody rigidBody;
    private Transform originalParentTransform;

    private AudioSource audioSource;
    public AudioClip[] soundList; // 0 = hitAudio, 1 = takeAudio, 2 = throwAudio

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Start()
    {
        originalParentTransform = transform.parent;
        audioSource = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 1f; // 3D sound
        audioSource.minDistance = 0.5f;
        audioSource.maxDistance = 1f;
        base.Start();
    }

    override public void Take(CapsulePlayer player)
    {
        // stop
        rigidBody.linearVelocity = Vector3.zero;
        rigidBody.angularVelocity = Vector3.zero;
        // disable
        collider.enabled = false;
        rigidBody.isKinematic = true;
        // reparent
        Transform handTransform = player.handPoint;
        transform.SetParent(handTransform);
        transform.position = handTransform.position;
        // reset pos
        if (collider.gameObject != gameObject)
            collider.gameObject.transform.position = handTransform.position;
        // play take sound
        audioSource.PlayOneShot(soundList[1]);
        // put in hand
        player.PutInHand(this);
    }
    
    override public void Throw(CapsulePlayer player)
    {
        // enable
        collider.enabled = true;
        rigidBody.isKinematic = false;
        // reparent
        transform.SetParent(originalParentTransform);
        // move the projectile
        transform.SetPositionAndRotation(player.throwPoint.position, player.throwPoint.rotation);

        // throw
        float throwCommonForce = player.throwMassBase + rigidBody.mass * player.throwMassInfluence;
        // throw the projectile
        rigidBody.AddForce(throwCommonForce * player.throwObjectForce * player.throwPoint.forward, ForceMode.Impulse);
        // throw the player
        player.playerBody.AddForce(throwCommonForce * player.throwPlayerForce * transform.forward, ForceMode.Impulse);

        // play throw sound
        audioSource.PlayOneShot(soundList[2]);

        // remove from hand
        player.ClearHand();
    }

    private void OnCollisionEnter(Collision collision)
    {
        audioSource.pitch = Random.Range(0.7f, 1.5f);
        audioSource.volume = 0.3f;
        audioSource.PlayOneShot(soundList[0]);
    }
}
