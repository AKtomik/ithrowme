using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class MissionIndicator : MonoBehaviour
{
    [SerializeField] private TypeWriter textIndicationWriter;
    [SerializeField] private TypeWriter textLocationWriter;
    public AudioSource bipNotifSound;

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
        if (bipNotifSound == null)
        {
            Debug.LogException(new Exception(" AddMissionAudio doesnt have a sound, put it in Editor in MissionManager "));
        }
        textIndicationWriter.CheckInit();// in case it does not itself
        textIndicationWriter.bipSound = bipNotifSound;
        textIndicationWriter.Typing(data.indicationText, 2.5f);
    }
    
    private void PutLocation()
    {
        textLocationWriter.CheckInit();// in case it does not itself
        textLocationWriter.Typing(data.locationText, 1.5f);
    }
}
