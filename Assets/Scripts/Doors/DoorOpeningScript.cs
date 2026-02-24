using UnityEngine;

public class DoorOpeningScript : MonoBehaviour
{
    public bool isOpened = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
        }
        
    }
}
