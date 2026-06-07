using UnityEngine;

public class ReferenceStore : MonoBehaviour
{
    public static ReferenceStore instance;

    [Header("Doors")]
		public DoorOpeningScript centerLifeDoor;
		public DoorOpeningScript centerTechnicalDoor;
		public DoorOpeningScript centerResearchDoor;
		public DoorOpeningScript centerControlDoor;
		
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
    }
}