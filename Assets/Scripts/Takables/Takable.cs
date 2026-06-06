using UnityEngine;
using UnityEngine.Audio;

public abstract class Takable : MonoBehaviour
{
    [Header("Takable")]
    [SerializeField] protected Collider takeCollider;
    [SerializeField] protected AudioMixer audioMixer;
    protected AudioSource audioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    virtual public void Start()
    {
        GameObject takeObject = takeCollider.gameObject;
        TakableReference takableReference = takeObject.AddComponent(typeof(TakableReference)) as TakableReference;
        takableReference.takable = this;
        audioSource = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
        audioSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Master/SFX")[0];
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 1f; // 3D sound
        audioSource.minDistance = 0.5f;
        audioSource.maxDistance = 1f;
    }

    abstract public void Take(CapsulePlayer player);
    abstract public void Throw(CapsulePlayer player);
}
