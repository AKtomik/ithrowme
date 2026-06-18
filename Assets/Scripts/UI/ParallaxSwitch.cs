using UnityEngine;

public class ParallaxSwitch : MonoBehaviour
{
    private GameObject[] farAwayObjects = new GameObject[] {};
    [SerializeField] private string disabledTag = "FarawayDisable";
    [SerializeField] private GameObject parallaxParent;
    [SerializeField] private bool disableDisable = false;
    [SerializeField] private bool logMsg = false;

	void Awake()
	{
		farAwayObjects = GameObject.FindGameObjectsWithTag(disabledTag);
	}

	private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (logMsg) Debug.Log("enable ghost parallax");
        if (!disableDisable)
            foreach (var obj in farAwayObjects)
                obj.SetActive(false);
        parallaxParent.SetActive(true);
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (logMsg) Debug.Log("disable ghost parallax");
        if (!disableDisable)
            foreach (var obj in farAwayObjects)
                obj.SetActive(true);
        parallaxParent.SetActive(false);
    }
}
