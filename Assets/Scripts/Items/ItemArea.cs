using Unity.VisualScripting;
using UnityEngine;

public class ItemArea : MonoBehaviour
{
    [SerializeField] private Transform insideParent;
    [SerializeField] private Transform outsideParent;
    [SerializeField] public bool itemLogDebug = false;
    [SerializeField] public bool playerLogDebug = false;

	void OnTriggerEnter(Collider other)
	{
        if (!other.enabled) return;
        if (other.CompareTag("Items")) {
            if (!other.TryGetComponent<TakableReference>(out var takableRef)) return;
            var item = takableRef.takable as TakableItem;
            if (item == null) return;
    		item.Reparent(insideParent);
            if (itemLogDebug) Debug.Log("item enter area:"+item.name);
        }
        else if (other.CompareTag("Player")) {
            if (!other.TryGetComponent<CapsulePlayer>(out var player)) return;
            if (!player.anythingInHand) return;
            var item = player.Hand as TakableItem;
            if (item == null) return;
    		item.Reparent(insideParent);
            if (playerLogDebug) Debug.Log("player enter area:"+item.name);
        }
	}

    private void OnTriggerExit(Collider other)
    {
        if (!outsideParent || !other.enabled) return;
        if (other.CompareTag("Items")) {
            if (!other.TryGetComponent<TakableReference>(out var takableRef)) return;
            var item = takableRef.takable as TakableItem;
            if (item == null) return;
    		item.Reparent(outsideParent);
            if (itemLogDebug) Debug.Log("item exit area:"+item.name);
        }
        else if (other.CompareTag("Player")) {
            if (!other.TryGetComponent<CapsulePlayer>(out var player)) return;
            if (!player.anythingInHand) return;
            var item = player.Hand as TakableItem;
            if (item == null) return;
    		item.Reparent(outsideParent);
            if (playerLogDebug) Debug.Log("player exit area:"+item.name);
        }
    }
}
