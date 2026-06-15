using TMPro;
using UnityEngine;

public class MissionIndicator : MonoBehaviour
{
    [SerializeField] private TypeWriter textIndicationWriter;
    [SerializeField] private TypeWriter textLocationWriter;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void PutData(MissionData data)
    {
        //textIndicationWriter.Typing(2.5f);
    }
}
