using UnityEngine;

public class AlarmScript : MonoBehaviour
{
    public bool isAlarmOn = true;
    public float vitesse = 180f;
    private void Update()
    {
        if (isAlarmOn)
        {
            
            transform.Rotate(0f, vitesse * Time.deltaTime, 0f, Space.Self);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
}
