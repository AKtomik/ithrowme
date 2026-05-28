using UnityEngine;

public abstract class ButtonTrigger : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (!this.enabled || !other.gameObject.CompareTag("Items")) return;
        OnTrigger();
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z / 4);
        this.enabled = false;
    }

    public abstract void OnTrigger();
}
