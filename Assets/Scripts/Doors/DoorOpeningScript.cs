using UnityEngine;

public class DoorOpeningScript : MonoBehaviour
{
    public bool isOpened = false;
    
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
            m_Animator.ResetTrigger("Close");
            m_Animator.SetTrigger("Open");
            isOpened = true;
            Invoke("ClosingDoors", 1f);
        }
        
    }
    public void ClosingDoors()
    {
        if (isOpened)
        {
            m_Animator.ResetTrigger("Open");
            m_Animator.SetTrigger("Close");
            isOpened = false;
        }
    }
}
