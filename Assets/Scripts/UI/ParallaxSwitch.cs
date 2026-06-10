using UnityEngine;

public class ParallaxSwitch : MonoBehaviour
{
    private GameObject[] farAwayObjects = new GameObject[] {};
    [SerializeField] private GameObject parallaxParent;
    [SerializeField] private bool disableDisable = false;

	void Awake()
	{
		farAwayObjects = GameObject.FindGameObjectsWithTag("FarawayDisable");
	}

	private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (!disableDisable)
            foreach (var obj in farAwayObjects)
                obj.SetActive(false);
        parallaxParent.SetActive(true);
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (!disableDisable)
            foreach (var obj in farAwayObjects)
                obj.SetActive(true);
        parallaxParent.SetActive(false);
    }
}
