using UnityEngine;

public abstract class ButtonTrigger : MonoBehaviour
{
    public bool ONE_TIME_TRIGGER = true;
    public Vector3 TRIGGER_SCALE = new Vector3(1, 1, .25f);

    private void OnCollisionEnter(Collision other)
    {
        if (!this.enabled || !other.gameObject.CompareTag("Items")) return;
        OnTrigger();
        transform.localScale = new Vector3(transform.localScale.x * TRIGGER_SCALE.x, transform.localScale.y * TRIGGER_SCALE.y, transform.localScale.z * TRIGGER_SCALE.z);
        if (ONE_TIME_TRIGGER)
        {
            this.enabled = false;
        }
    }

    public abstract void OnTrigger();
}
