using UnityEngine;

public class BackDoorScript : MonoBehaviour
{
    [SerializeField] private DoorOpeningScript doorOpening;

	void OnTriggerEnter(Collider other)
	{
        doorOpening.BackdoorTriggerEnter(other);
	}
}
