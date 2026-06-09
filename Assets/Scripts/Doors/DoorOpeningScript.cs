using System.Collections.Generic;
using UnityEngine;

public class DoorOpeningScript : MonoBehaviour
{
    [Header("State")]
    // if true, the door cannot be open at all
    [SerializeField] private bool locked = false;
    [SerializeField] private bool opened = false;
    
    [Header("Auto Open")]
    [SerializeField] public bool automaticOpening = true; // if false, we can only open we a button
    [SerializeField] public bool detectPlayer = true;
    [SerializeField] public bool detectItems = true;

    [Header("Sounds")]
    [SerializeField] private AudioSource doorSlam; // audio both for opening and closing
    [SerializeField] private AudioClip[] audioClips; // 0 = open , 1 = close


    public List<GameObject> objectsInDoorRanch = new List<GameObject>(); // the list of the objects currently in the door trigger box
    
    private Animator m_Animator;
    private Collider m_Collider;
    void Start()
    {
     m_Animator = GetComponentInChildren<Animator>();
     m_Collider = GetComponentInChildren<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// maybe a single function ?? 
    /// flip flop type with isOpened
    public void OpeningDoors()
    {
        if (opened || locked) return;
        doorSlam.PlayOneShot(audioClips[0]);
        m_Animator.ResetTrigger("Close");
        m_Animator.SetTrigger("Open");
        opened = true;
    }

    public void ClosingDoors()
    {
        if (!opened) return;
        doorSlam.PlayOneShot(audioClips[1]);
        m_Animator.ResetTrigger("Open");
        m_Animator.SetTrigger("Close");
        opened = false;
    }
    public bool isOpened()
    {
        return opened;
    }
    
    public void LockingDoors(bool lockOpenState = false)
    {
        // lock
        locked = true;
        m_Collider.enabled = false;
        // opening
        if (opened != lockOpenState)
        {
            if (lockOpenState)
                OpeningDoors();
            else
                ClosingDoors();
        }
        // model modif
    }
    public void UnlockingDoors(bool lockOpenState = false)
    {
        // lock
        locked = false;
        m_Collider.enabled = true;
        // opening
        if (opened != lockOpenState)
        {
            if (lockOpenState)
                OpeningDoors();
            else
                ClosingDoors();
        }
        // model modif
    }
    public bool IsLocked()
    {
        return locked;
    }

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

    private void OnTriggerExit(Collider other)
    {
        if (objectsInDoorRanch.Contains(other.gameObject))
            objectsInDoorRanch.Remove(other.gameObject);

        if (automaticOpening && (other.gameObject.CompareTag("Player") || objectsInDoorRanch.Count == 0))
        {
            ClosingDoors();
            CancelInvoke("ClosingDoors");
        }
    }
}
