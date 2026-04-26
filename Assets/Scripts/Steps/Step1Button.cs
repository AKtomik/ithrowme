using UnityEngine;

public class Step1Button : MonoBehaviour
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
        //Debug.Log("step 1 colliding with "+ other.gameObject.layer);
        if (!other.gameObject.CompareTag("Items")) return;
        Debug.Log("step 1 completed");
        missionManager.CompleteMission(1);
        missionManager.AddMission(2);
    }
}
