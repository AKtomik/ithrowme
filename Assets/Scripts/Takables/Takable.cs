using UnityEngine;

public abstract class Takable : MonoBehaviour
{
    [SerializeField] protected new Collider collider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    virtual public void Start()
    {
        GameObject takeObject = collider.gameObject;
        TakableReference takableReference = takeObject.AddComponent(typeof(TakableReference)) as TakableReference;
        takableReference.takable = this;
    }

    abstract public void Take(CapsulePlayer player);
    abstract public void Throw(CapsulePlayer player);
}
