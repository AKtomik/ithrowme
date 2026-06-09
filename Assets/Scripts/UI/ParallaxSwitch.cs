using UnityEngine;

public class ParallaxSwitch : MonoBehaviour
{
    private GameObject[] farAwayObjects = new GameObject[] {};

	void Awake()
	{
		farAwayObjects = GameObject.FindGameObjectsWithTag("FarawayDisable");
	}

	private void OnTriggerEnter()
    {
        foreach (var obj in farAwayObjects)
        {
            obj.SetActive(false);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        foreach (var obj in farAwayObjects)
        {
            obj.SetActive(true);
        }
    }
}
