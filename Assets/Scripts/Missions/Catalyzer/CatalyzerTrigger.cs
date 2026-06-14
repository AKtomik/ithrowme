using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class CatalyzerTrigger : MonoBehaviour
{
    [Header("Catalyzer Trigger")]
    public bool oneTimeTrigger = true;
    public Transform putTransform;
    [Header("Catalyzer Rotation")]
    public float rotationMinSpeed = 470f;
    public float rotationStartSpeed = 1470f;
    public float rotationSlowSpeed = 200f;
    
    private float speed = 0;
    private Collider collid;

	public void Start()
	{
        collid = GetComponent<Collider>();
        speed = 0;
	}

	public void Update()
	{
        if (speed == 0) return;
        if (speed > rotationMinSpeed) speed -= rotationSlowSpeed * Time.deltaTime;
		putTransform.Rotate(new Vector3(0, speed * Time.deltaTime, 0));
	}

	public void OnTriggerEnter(Collider other)
	{
        // check
        if (!enabled) return;
        if (!other.gameObject.TryGetComponent(out CatalyzableItem catalyzable)) return;
        if (!other.gameObject.TryGetComponent(out TakableReference takableReference)) return;
        var takableItem = takableReference.takable as TakableItem;
        
        // put
        takableItem.Put(putTransform);
        speed = rotationStartSpeed;

        // trigger
        OnTrigger();
        if (oneTimeTrigger) collid.enabled = false;
    }

    public abstract void OnTrigger();
}
