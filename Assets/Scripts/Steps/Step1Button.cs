using UnityEngine;

public class Step1Button : MonoBehaviour
{
    public DoorOpeningScript doorScript;
    public MissionManager missionManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other) {
        if (!this.enabled || !other.gameObject.CompareTag("Items")) return;
        Debug.Log("Step1Button: step 1 completed");
        doorScript.OpeningDoors();
        missionManager.CompleteMission(1);
        missionManager.AddMission(2);
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z / 4);
        this.enabled = false;
    }
}
