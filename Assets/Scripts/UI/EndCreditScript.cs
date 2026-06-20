using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndCreditScript : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 40f;
    [SerializeField] private float fadeDuration = 2f;
    [SerializeField] private RectTransform creditTransform;
    [SerializeField] private AudioSource creditAudio;
    [SerializeField] private RawImage endBlackScreen;
    private bool isEndind = false;
    private void Start()
    {
        creditTransform.gameObject.SetActive(false);

        Color color = endBlackScreen.color;
        color.a = 0f;
        endBlackScreen.color = color;
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
        Invoke("EndingT", 32);
    }

    public void EndingT()
    {
        StartCoroutine(FadeToBlack());
    }

    private IEnumerator FadeToBlack()
    {
        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;

            Color color = endBlackScreen.color;
            color.a = Mathf.Clamp01(timer / fadeDuration);
            endBlackScreen.color = color;

            yield return null;
        }

        Color finalColor = endBlackScreen.color;
        finalColor.a = 1f;
        endBlackScreen.color = finalColor;

        yield return new WaitForSeconds(2);

        SceneManager.LoadScene("MainMenuLevel");
    }

    
}
