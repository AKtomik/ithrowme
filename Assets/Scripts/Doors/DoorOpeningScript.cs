using UnityEngine;

public class DoorOpeningScript : MonoBehaviour
{
    public bool isOpened = false;
    
    private Animator m_Animator;
    void Start()
    {
     m_Animator = GetComponent<Animator>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OpeningDoors()
    {
        if (!isOpened)
        {
            m_Animator.SetTrigger("Open");
            isOpened = true;
            Invoke("ClosingDoors", 1f);
        }
        
    }
    public void ClosingDoors()
    {
        if (isOpened)
        {
            m_Animator.SetTrigger("Close");
            isOpened = false;
        }
    }
}
