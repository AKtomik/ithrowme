using Unity.VisualScripting;
using UnityEngine;

public class ItemArea : MonoBehaviour
{
    [SerializeField] private Transform insideParent;
    [SerializeField] private Transform outsideParent;
    [SerializeField] public bool itemLogDebug = false;

	void OnTriggerEnter(Collider other)
	{
        if (!other.CompareTag("Items") || !other.enabled) return;
        //if (!other.TryGetComponent<Rigidbody>(out var rb) || rb.isKinematic) return;
        //if (other.transform.parent == insideParent) return;
        if (!other.TryGetComponent<TakableReference>(out var takableRef)) return;
        Takable item = takableRef.takable;
        if (itemLogDebug) Debug.Log("item enter area:"+item.name);
		item.transform.SetParent(insideParent);
	}

    private void OnTriggerExit(Collider other)
    {
        if (!outsideParent || !other.CompareTag("Items") || !other.enabled) return;
        //if (!other.TryGetComponent<Rigidbody>(out var rb) || rb.isKinematic) return;
        //if (other.transform.parent != insideParent) return;
        if (!other.TryGetComponent<TakableReference>(out var takableRef)) return;
        Takable item = takableRef.takable;
        if (itemLogDebug) Debug.Log("item exit area:"+item.name);
		item.transform.SetParent(outsideParent);
    }
}
