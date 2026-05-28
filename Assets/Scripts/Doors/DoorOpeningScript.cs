using System.Collections.Generic;
using UnityEngine;

public class DoorOpeningScript : MonoBehaviour
{
    [Header("State")]
    public bool locked = false; // if true, the door cannot be open at all
    public bool isOpened = false;
    
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
        if (!isOpened && !locked)
        {
            doorSlam.Play();
            m_Animator.ResetTrigger("Close");
            m_Animator.SetTrigger("Open");
            isOpened = true;

        }

    }
    public void ClosingDoors()
    {
        if (isOpened)
        {
            doorSlam.Play();
            m_Animator.ResetTrigger("Open");
            m_Animator.SetTrigger("Close");
            isOpened = false;
        }
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
