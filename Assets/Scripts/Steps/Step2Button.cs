using UnityEngine;

public class Step2Button : MonoBehaviour
{
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
        Debug.Log("Step2Button: step 2 completed");
        missionManager.CompleteMission(2);
        missionManager.AddMission(3);
        missionManager.AddMission(4);
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z / 4);
        this.enabled = false;
    }
}
