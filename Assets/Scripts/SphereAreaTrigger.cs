using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SphereAreaTrigger : MonoBehaviour
{
    private List<GameObject> takeObjects = new();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {}

    // Update is called once per frame
    void Update() {}

	void OnTriggerEnter(Collider other)
	{
		if (!other.TryGetComponent(out TakableObject hitTake)) return;
		takeObjects.Add(other.gameObject);
	}

	void OnTriggerExit(Collider other)
	{
        GameObject otherObject = other.gameObject;
		if (!takeObjects.Contains(otherObject)) return;
		takeObjects.Remove(otherObject);
	}

	public GameObject[] AllObjectsInRange()
    {
        return takeObjects.ToArray();
    }

    public GameObject GetNearestObject()
    {
        GameObject takeObject = null;
        float takeDistance = 100;
        foreach (var loopObject in AllObjectsInRange())
        {
			if (!loopObject.TryGetComponent(out TakableObject loopTake)) continue;
			float objectDistance = Vector3.Distance(transform.position, loopObject.transform.position);
            if (!(objectDistance < takeDistance)) continue;
            takeObject = loopObject;
            takeDistance = objectDistance;
        }
        return takeObject;
    }
    
    public int AmountInRange()
    {
        return AllObjectsInRange().Count();
    }

    public bool HasObjectInRange()
    {
        return AmountInRange() != 0;
    }
}
