using UnityEngine;

public abstract class CatalyzerTrigger : MonoBehaviour
{
    public bool oneTimeTrigger = true;
    public Transform putTransform;

    public void OnTriggerEnter(Collider other)
	{
        // check
        if (!this.enabled) return;
        Debug.Log("hello in", other);
        if (!other.gameObject.TryGetComponent(out CatalyzableItem catalyzable)) return;
        Debug.Log("catalyzable took", catalyzable);
        if (!other.gameObject.TryGetComponent(out TakableReference takableReference)) return;
        Debug.Log("takableReference took", takableReference);
        var takableItem = takableReference.takable as TakableItem;
        Debug.Log("takableItem took", takableItem);
        
        // put
        takableItem.Put(putTransform);

        // trigger
        OnTrigger();
        if (oneTimeTrigger) this.enabled = false;
    }

    public abstract void OnTrigger();
}
