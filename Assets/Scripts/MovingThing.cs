using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovingThing : MonoBehaviour
{
    private Rigidbody rb;
    private bool wasKinematic;
    private bool frozen = false;
    private Vector3 frozenLinearVelocity;
    private Vector3 frozenAngularVelocity;

    // self add
    static List<MovingThing> allMovingThings = new();
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        allMovingThings.Add(this);
    }

	void OnDestroy()
	{
		allMovingThings.Remove(this);
	}

    // self freeze
    void FreezeMove()
    {
        if (frozen)
        {
            Debug.LogWarning("freezing already frozen thing");
            return;
        }
        frozen = true;
        frozenLinearVelocity = rb.linearVelocity;
        frozenAngularVelocity = rb.angularVelocity;
        wasKinematic = rb.isKinematic;
        rb.isKinematic = true;
    }
    
    void UnfreezeMove()
    {
        if (!frozen)
        {
            Debug.LogWarning("unfreezing already not frozen thing");
            return;
        }
        frozen = false;
        rb.isKinematic = wasKinematic;
        if (!rb.isKinematic)
        {
            rb.linearVelocity = frozenLinearVelocity;
            rb.angularVelocity = frozenAngularVelocity;
        }
    }

    // all freeze
    public static void FreezeAll()
    {
        foreach (var moving in allMovingThings)
            moving.FreezeMove();
    }
    
    public static void UnfreezeAll()
    {
        foreach (var moving in allMovingThings)
            moving.UnfreezeMove();
    }
}
