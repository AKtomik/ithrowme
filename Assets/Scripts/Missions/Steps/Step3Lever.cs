using UnityEngine;

public class Step3Lever : TakableLever
{
    [Header("Step Pointers")]
    public MissionManager missionManager;
    
    [SerializeField] private MeshRenderer consoleLedMesh;
    [SerializeField] private Material consoleLedFirstMaterial;
    [SerializeField] private Material consoleLedDoneMaterial;
    
    [SerializeField] private MeshRenderer capsuleLedMesh;
    [SerializeField] private Material capsuleLedFirstMaterial;
    [SerializeField] private Material capsuleLedDoneMaterial;

    new private void Start() {
        base.Start();
        consoleLedMesh.material = consoleLedFirstMaterial;
        capsuleLedMesh.material = capsuleLedFirstMaterial;
    }

    public override void PullStart(CapsulePlayer player) {
        Debug.Log("Step3Lever: pulling...");
        missionManager.CompleteMission(3);
    }
    public override void PullFinish(CapsulePlayer player)
    {
        Debug.Log("Step3Lever: step 3 completed");
        ReferenceSingleton.instance.consoleClick.CheckFinalMission();

        // material
        consoleLedMesh.material = consoleLedDoneMaterial;
        capsuleLedMesh.material = capsuleLedDoneMaterial;
        //DynamicGI.SetEmissive(capsuleLedMesh, capsuleLedMesh.material.GetColor("_EmissionColor"));
    }
}
