using UnityEngine;

public class DoorOpeningScript : MonoBehaviour
{
    public bool isOpened = false;
    public bool automaticOpening = true;

    [SerializeField] private AudioSource doorSlam;
    
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
        if (!isOpened)
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
        
        if (automaticOpening && (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Items")))
        {
            OpeningDoors();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (automaticOpening && (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Items")))
        {
            ClosingDoors();
        }
    }
}
