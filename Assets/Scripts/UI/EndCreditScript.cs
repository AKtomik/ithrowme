using UnityEngine;

public class EndCreditScript : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 40f;
    [SerializeField] private RectTransform creditTransform;
    [SerializeField] private AudioSource creditAudio;

    private void Start()
    {
        creditAudio.Play();
    }
    private void Update()
    {
        creditTransform.anchoredPosition += new Vector2(0, scrollSpeed * Time.deltaTime);
    }
}
