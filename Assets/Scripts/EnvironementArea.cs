using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnvironementArea : MonoBehaviour
{
  [SerializeField] private bool disableOnlyRenders = false;
  [SerializeField] private GameObject environementParent;
  [SerializeField] private GameObject throwableParent;
  [SerializeField] private GameObject potentialParallaxParent;
  [SerializeField] private bool defaultHide = false;
  [SerializeField] private bool triggerIsHidding = false;

	private Dictionary<Renderer, bool> renderers = new();
	private bool showning;

	void Start()
	{
	  //renderers = environementParent.GetComponentsInChildren<Behaviour>(true);
	  //renderers = new Behaviour[] {};
		//renderers.AddRange(environementParent.GetComponentsInChildren<Canvas>(true));
		//Debug.Log("renderers:"+renderers.Count);
		if (disableOnlyRenders)
		{
			foreach (var render in environementParent.GetComponentsInChildren<Renderer>(true))
				renderers[render] = render.enabled;
			if (throwableParent)
			{
				foreach (var render in throwableParent.GetComponentsInChildren<Renderer>(true))
					renderers[render] = render.enabled;
			}
		}
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
		//Debug.Log("SetShow:"+renderers.Count+"of"+doShow);
		if (showning == doShow) return;
		showning = doShow;

		if (disableOnlyRenders)
		{

			foreach(var key in renderers.Keys.ToArrayPooled())
			{
				if(key == null)
				{
					renderers.Remove(key);
				}
			}

			if (doShow)
				foreach (var render in renderers.Keys.ToArrayPooled())
				{
					if (render)
					{
						render.enabled = renderers[render];
					}
				}
			else
				foreach (var render in renderers.Keys.ToArrayPooled())
				{
					if (render)
					{
						renderers[render] = render.enabled;
						render.enabled = false;
					}
				}

		} else {
			environementParent.SetActive(doShow);
			if (throwableParent) throwableParent.SetActive(doShow);
		}

		if (potentialParallaxParent) 
			potentialParallaxParent.SetActive(!doShow);
	}
}
