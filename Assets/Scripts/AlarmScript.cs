using UnityEngine;

public class AlarmScript : MonoBehaviour
{
    public bool isAlarmOn = true;

    private void Update()
    {
        if (isAlarmOn)
        {
            float vitesse = 180f; // degrés par seconde
            transform.Rotate(0f, vitesse * Time.deltaTime, 0f, Space.Self);
        }
        
    }
}
