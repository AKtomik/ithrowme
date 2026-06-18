using UnityEngine;

public class EnvironementArea : MonoBehaviour
{
  [SerializeField] private GameObject environementParent;
  [SerializeField] private GameObject potentialParallaxParent;
  [SerializeField] private bool defaultHide = false;
  [SerializeField] private bool triggerIsHidding = false;

	private bool showning;
	//private bool oldState;

	void Start()
	{
		SetShow(!defaultHide);
	}

	void OnTriggerEnter(Collider other)
	{
		if (!other.gameObject.CompareTag("Player")) return;
		SetShow(true ^ triggerIsHidding);
	}
	
	void OnTriggerExit(Collider other)
	{
		if (!other.gameObject.CompareTag("Player")) return;
		SetShow(false ^ triggerIsHidding);
	}

	void SetShow(bool doShow)
	{
		showning = doShow;
		environementParent.SetActive(doShow);
		if (potentialParallaxParent) 
			potentialParallaxParent.SetActive(!doShow);
	}
}
