using UnityEngine;

public abstract class ButtonTrigger : MonoBehaviour
{
    [Header("Trigger Settings")]
    public bool oneTimeTrigger = true;
    public Vector3 triggerScale = new Vector3(1, 1, .25f);

    private void OnCollisionEnter(Collision other)
    {
        if (!this.enabled || !other.gameObject.CompareTag("Items")) return;
        OnTrigger();
        transform.localScale = new Vector3(transform.localScale.x * triggerScale.x, transform.localScale.y * triggerScale.y, transform.localScale.z * triggerScale.z);
        if (oneTimeTrigger)
        {
            this.enabled = false;
        }
    }

    public abstract void OnTrigger();
}
