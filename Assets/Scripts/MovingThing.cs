using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class MovingThing : MonoBehaviour
{
    static List<MovingThing> allMovingThings = new();

    // self add
    protected Collider movingCollider;
    void Awake()
    {
        movingCollider = GetComponent<Collider>();
        allMovingThings.Add(this);
    }

	void OnDestroy()
	{
		allMovingThings.Remove(this);
	}

    // self pause
    //void Pause
}
