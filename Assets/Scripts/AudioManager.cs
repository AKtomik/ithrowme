using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource ambianceSound;

    public void StartAmbiance()
    {
        Invoke("PlayAmbiance", 8f);
    }


    private void PlayAmbiance()
    {
        ambianceSound.Play();
        Invoke("PlayAmbiance", Random.Range(30f, 60f));
    }
    
    public void StopAmbiance()
    {
        CancelInvoke();
    }

}
