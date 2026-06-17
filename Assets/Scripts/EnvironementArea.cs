using UnityEngine;

public class EnvironementArea : MonoBehaviour
{
  [SerializeField] private GameObject environementParent;
  [SerializeField] private bool defaultHide = false;

	private bool showning;
	//private bool oldState;

	void Start()
	{
		SetShow(!defaultHide);
	}

	void OnTriggerEnter(Collider other)
	{
		if (!other.gameObject.CompareTag("Player")) return;
		SetShow(true);
	}
	
	void OnTriggerExit(Collider other)
	{
		if (!other.gameObject.CompareTag("Player")) return;
		SetShow(false);
	}

	void SetShow(bool doShow)
	{
		showning = doShow;
		environementParent.SetActive(doShow);
	}
}
