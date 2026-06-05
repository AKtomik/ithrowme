using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class CanvasBoss : MonoBehaviour
{
    [SerializeField] private GameObject cinematicBars;
    [SerializeField] private GameObject missionsContainer;



    private void Start()
    {
        Canvas.ForceUpdateCanvases();

    }
    private void Update()
    {
        Canvas.ForceUpdateCanvases();
        LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
        LayoutRebuilder.ForceRebuildLayoutImmediate(missionsContainer.GetComponent<RectTransform>());
    }

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
