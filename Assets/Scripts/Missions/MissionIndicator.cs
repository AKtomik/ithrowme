using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class MissionIndicator : MonoBehaviour
{
    [SerializeField] private TypeWriter textIndicationWriter;
    [SerializeField] private TypeWriter textLocationWriter;

    private MissionData data;

    public async void PutData(MissionData missionData)
    {
        this.data = missionData;
        PutIndication();
        await Task.Delay((int)(2.5 * 1000));
        PutLocation();
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
        textLocationWriter.Typing(data.locationText, 1.5f);
    }
}
