using UnityEngine;

public class CanvasBoss : MonoBehaviour
{
    [SerializeField] private GameObject cinematicBars;
    [SerializeField] private GameObject missionsContainer;

    /*
    private MeshCollider meshCollider;

    private void Start()
    {
        meshCollider = GetComponent<MeshCollider>();
        if (meshCollider)
        {
            Destroy(meshCollider);
        }
    }
    */
    public void EnableCinematic()
    {// TODO: bar progressive enter with time parameter
        cinematicBars.SetActive(true);
        missionsContainer.SetActive(false);
    }
    
    public void DisableCinematic()
    {
        cinematicBars.SetActive(false);
        missionsContainer.SetActive(true);
    }
}
