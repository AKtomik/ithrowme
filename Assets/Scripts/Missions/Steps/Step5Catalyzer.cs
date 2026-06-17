using Unity.VisualScripting;
using UnityEngine;

public class Step5Catalyzer : CatalyzerTrigger
{
    [Header("Step Pointers")]
    public MissionManager missionManager;
    [SerializeField] private AudioSource cubeSound;
    [SerializeField] private MeshRenderer powerNeonMesh;
    [SerializeField] private Material powerNeonEnergyMaterial;
    [SerializeField] private GameObject[] powerLights;

    public override void OnTrigger() {

        if (cubeSound)
        {
            cubeSound.Play();
        }

        Debug.Log("Step5Catalyzer: step 5 completed");
        
        // missions
        missionManager.CompleteMission(5);
        
        if (!ReferenceSingleton.instance.consoleClick.CheckFinalMission()) {
        
        // doors
            // in case of lockUnreleatedDoor (don't need to check)
            ReferenceSingleton.instance.centerResearchDoor.UnlockingDoors();
        }

        if (SettingsStore.smartLockFinishedDoor)
        {
            ReferenceSingleton.instance.centerTechnicalDoor.LockingDoors();
        }

        // environement
        powerNeonMesh.material = powerNeonEnergyMaterial;
        DynamicGI.SetEmissive(powerNeonMesh, powerNeonEnergyMaterial.GetColor("_EmissionColor"));
        foreach (var obj in powerLights) obj.SetActive(true);
    }
}
