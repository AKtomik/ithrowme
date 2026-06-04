using UnityEngine;

public abstract class CatalyzerTrigger : MonoBehaviour
{
    public bool oneTimeTrigger = true;
    public Transform putTransform;

    public void OnTriggerEnter(UnityEngine.Collider other)
	{
        // check
        if (!this.enabled) return;
        if (!other.gameObject.TryGetComponent(out CatalyzableItem catalyzable)) return;
        if (!other.gameObject.TryGetComponent(out TakableReference takableReference)) return;
        var takableItem = takableReference.takable as TakableItem;
        
        // put
        takableItem.Put(putTransform);

        // trigger
        OnTrigger();
        if (oneTimeTrigger) this.enabled = false;
    }

    public abstract void OnTrigger();
}
