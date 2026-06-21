using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnvironementArea : MonoBehaviour
{
  [SerializeField] private GameObject environementParent;
  [SerializeField] private GameObject potentialParallaxParent;
  [SerializeField] private bool defaultHide = false;
  [SerializeField] private bool triggerIsHidding = false;

	private Dictionary<Renderer, bool> renderers = new();
	private bool showning;

	void Start()
	{
	  //renderers = environementParent.GetComponentsInChildren<Behaviour>(true);
	  //renderers = new Behaviour[] {};
		foreach (var render in environementParent.GetComponentsInChildren<Renderer>(true))
		{
			renderers[render] = render.enabled;
		}
		//renderers.AddRange(environementParent.GetComponentsInChildren<Canvas>(true));
		//Debug.Log("renderers:"+renderers.Count);
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

		if (potentialParallaxParent) 
			potentialParallaxParent.SetActive(!doShow);
	}
}
