using UnityEngine;

public class EndCreditScript : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 40f;
    [SerializeField] private RectTransform creditTransform;
    [SerializeField] private AudioSource creditAudio;
    private bool isEndind = false;
    private void Start()
    {
        creditTransform.gameObject.SetActive(false);
    }


    private void Update()
    {
        if (isEndind && creditTransform.anchoredPosition.y < 3055.17f)
        {
            creditTransform.anchoredPosition += new Vector2(0, scrollSpeed * Time.deltaTime);
        }
        
    }

    public void StartEnding()
    {
        creditTransform.gameObject.SetActive(true);
        isEndind = true;
        creditAudio.Play();
    }
}
