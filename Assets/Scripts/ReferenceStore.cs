using UnityEngine;

public class ReferenceStore : MonoBehaviour
{
    public static ReferenceStore instance;

    [Header("Main")]
		public CapsulePlayer player;
		public MissionManager missionManager;
    [Header("Center Doors")]
		public DoorOpeningScript centerLifeDoor;
		public DoorOpeningScript centerTechnicalDoor;
		public DoorOpeningScript centerResearchDoor;
		public DoorOpeningScript centerControlDoor;
    [Header("Special Doors")]
		public DoorOpeningScript emergencyLifeDoor;
		public DoorOpeningScript emergencyGravityDoor;
		
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
    }
}