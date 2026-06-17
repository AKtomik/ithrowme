using UnityEngine;

public class EnvironementArea : MonoBehaviour
{
  [SerializeField] private GameObject environementParent;
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
		Debug.Log("true ^ triggerIsHidding = "+true+triggerIsHidding+(true ^ triggerIsHidding));
		SetShow(true ^ triggerIsHidding);
	}
	
	void OnTriggerExit(Collider other)
	{
		if (!other.gameObject.CompareTag("Player")) return;
		Debug.Log("false ^ triggerIsHidding = "+false+triggerIsHidding+(false ^ triggerIsHidding));
		SetShow(false ^ triggerIsHidding);
	}

	void SetShow(bool doShow)
	{
		showning = doShow;
		environementParent.SetActive(doShow);
	}
}
