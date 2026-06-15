using TMPro;
using UnityEngine;

public class MissionIndicator : MonoBehaviour
{
    [SerializeField] private TypeWriter textIndicationWriter;
    [SerializeField] private TypeWriter textLocationWriter;

    private MissionData data;

    public void PutData(MissionData missionData)
    {
        this.data = missionData;
        PutIndication();
        StartCoroutine(nameof(PutLocation), 2.5f);
    }

    public void Kill()
    {
        Destroy(gameObject);
    }

    private void PutIndication()
    {
        textIndicationWriter.Typing(data.indicationText, 2.5f);
    }
    
    private void PutLocation()
    {
        textIndicationWriter.Typing(data.indicationText, 1.5f);
    }
}
