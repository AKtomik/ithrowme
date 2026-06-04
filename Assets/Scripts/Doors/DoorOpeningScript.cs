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

    public List<GameObject> objectsInDoorRanch = new List<GameObject>(); // the list of the objects currently in the door trigger box
    
    private Animator m_Animator;
    void Start()
    {
     m_Animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// maybe a single function ?? 
    /// flip flop type with isOpened
    public void OpeningDoors()
    {
        if (!opened && !locked)
        {
            doorSlam.Play();
            m_Animator.ResetTrigger("Close");
            m_Animator.SetTrigger("Open");
            opened = true;

        }

    }
    public void ClosingDoors()
    {
        if (opened)
        {
            doorSlam.Play();
            m_Animator.ResetTrigger("Open");
            m_Animator.SetTrigger("Close");
            opened = false;
        }
    }
    public bool isOpened()
    {
        return opened;
    }
    
    public void LockingDoors()
    {
        locked = true;
        // show up locked state on the model
    }
    public void UnlockingDoors()
    {
        locked = false;
        // show up unlocked state on the model
    }
    public bool isLocked()
    {
        return locked;
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (automaticOpening && detectPlayer && other.gameObject.CompareTag("Player"))
        {
            OpeningDoors();
            CancelInvoke("ClosingDoors");
            objectsInDoorRanch.Add(other.gameObject);
        }
        else if (automaticOpening && detectItems && other.gameObject.CompareTag("Items"))
        {
            OpeningDoors();
            CancelInvoke("ClosingDoors");
            Invoke("ClosingDoor", 10);
            objectsInDoorRanch.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (objectsInDoorRanch.Contains(other.gameObject))
        {
            objectsInDoorRanch.Remove(other.gameObject);
        }


        if (automaticOpening && (other.gameObject.CompareTag("Player") || objectsInDoorRanch.Count == 0))
        {
            ClosingDoors();
            CancelInvoke("ClosingDoors");
        }
    }
}
