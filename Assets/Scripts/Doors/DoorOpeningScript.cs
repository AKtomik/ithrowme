using System.Collections.Generic;
using UnityEngine;

public class DoorOpeningScript : MonoBehaviour
{
    [Header("Pointers")]
    [SerializeField] private Collider triggerCollider;
    [SerializeField] private GameObject solidCollidObject;
    [SerializeField] private Animator[] animators;

    [Header("State")]
    // if true, the door cannot be open at all
    [SerializeField] private bool locked = false;
    [SerializeField] private bool opened = false;
    
    [Header("Auto Open")]
    [SerializeField] public bool automaticOpening = true; // if false, we can only open we a button
    [SerializeField] public bool detectPlayer = true;
    [SerializeField] public bool detectItems = true;

    [Header("Material")]
    [SerializeField] private Renderer[] lightRenderers;
    [SerializeField] private Material lockedMaterial;
    [SerializeField] private Material unlockedMaterial;

    [Header("Sounds")]
    [SerializeField] private AudioSource doorSlam; // audio both for opening and closing
    [SerializeField] private AudioClip[] audioClips; // 0 = open , 1 = close

    private List<GameObject> objectsInDoorRanch = new List<GameObject>(); // the list of the objects currently in the door trigger box
    
    // LOOP
    void Start()
    {
        RefreshLockMaterial();
    }

    void Update() {}

    // OPEN
    /// maybe a single function ?? 
    /// flip flop type with isOpened
    public void OpeningDoors(bool skipLocked = false)
    {
        if (opened || (!skipLocked && locked)) return;
        doorSlam.PlayOneShot(audioClips[0]);
        foreach (var animator in animators)
        {
            animator.ResetTrigger("Close");
            animator.SetTrigger("Open");
        }
        solidCollidObject.SetActive(false);
        opened = true;
    }

    public void ClosingDoors()
    {
        if (!opened) return;
        doorSlam.PlayOneShot(audioClips[1]);
        foreach (var animator in animators)
        {
            animator.ResetTrigger("Open");
            animator.SetTrigger("Close");
        }
        solidCollidObject.SetActive(true);
        opened = false;
    }

    public bool isOpened()
    {
        return opened;
    }

    // LOCK    
    public void LockingDoors(bool lockOpenState = false)
    {
        // lock
        locked = true;
        //triggerCollider.enabled = false;// can't disable for an edgecase (backdoor)
        
        // opening
        if (opened != lockOpenState)
        {
            if (lockOpenState)
                OpeningDoors();
            else
                ClosingDoors();
        }

        // material
        RefreshLockMaterial();
    }
    
    public void UnlockingDoors(bool lockOpenState = false)
    {
        // lock
        locked = false;
        //triggerCollider.enabled = true;// can't disable for an edgecase (backdoor)
        // opening
        if (opened != lockOpenState)
        {
            if (lockOpenState)
                OpeningDoors();
            else
                ClosingDoors();
        }
        
        // material
        RefreshLockMaterial();
    }

    public bool IsLocked()
    {
        return locked;
    }

    public void RefreshLockMaterial()
    {
        Material material = locked ? lockedMaterial : unlockedMaterial;
        foreach (var renderer in lightRenderers)
        {
            renderer.material = material;
            DynamicGI.SetEmissive(renderer, renderer.material.GetColor("_EmissionColor"));
        }
    }

    // TRIGGERS
    private void OnTriggerEnter(Collider other)
    {
        if (automaticOpening && detectPlayer && other.gameObject.CompareTag("Player"))
        {
            OpeningDoors();
            CancelInvoke("ClosingDoors");
            if (!objectsInDoorRanch.Contains(other.gameObject))
                objectsInDoorRanch.Add(other.gameObject);
        }
        else if (automaticOpening && detectItems && other.gameObject.CompareTag("Items"))
        {
            OpeningDoors();
            CancelInvoke("ClosingDoors");
            Invoke("ClosingDoor", 10);
            if (!objectsInDoorRanch.Contains(other.gameObject))
                objectsInDoorRanch.Add(other.gameObject);
        }
        else if (automaticOpening && other.gameObject.GetComponentInParent<ObjectCanOpenDoor>() != null)
        {
            OpeningDoors();
            CancelInvoke("ClosingDoors");
            Invoke("ClosingDoor", 10);
            if (!objectsInDoorRanch.Contains(other.gameObject))
                objectsInDoorRanch.Add(other.gameObject);
        }
    }
    
    private void OnTriggerStay(Collider other)
    {// necessary for an edge case
        if (automaticOpening && detectPlayer && other.gameObject.CompareTag("Player"))
        {
            OpeningDoors();
            CancelInvoke("ClosingDoors");
            if (!objectsInDoorRanch.Contains(other.gameObject))
                objectsInDoorRanch.Add(other.gameObject);
        }
    }
    
    public void BackdoorTriggerEnter(Collider other)
    {// ! the backdoor trigger collision have to be inside the normal trigger collider
        if (automaticOpening && detectPlayer && other.gameObject.CompareTag("Player"))
        {
            OpeningDoors(true);
            CancelInvoke("ClosingDoors");
            if (!objectsInDoorRanch.Contains(other.gameObject))
                objectsInDoorRanch.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!objectsInDoorRanch.Contains(other.gameObject)) return;
        objectsInDoorRanch.Remove(other.gameObject);

        if (automaticOpening && (other.gameObject.CompareTag("Player") || objectsInDoorRanch.Count == 0))
        {
            ClosingDoors();
            CancelInvoke("ClosingDoors");
        }
    }
}
