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
    [SerializeField] private PauseManager pauseManager;
    
    public bool isEnding = false;
    private void Start()
    {
        creditTransform.gameObject.SetActive(false);

        Color color = endBlackScreen.color;
        color.a = 0f;
        endBlackScreen.color = color;

        endBlackScreen.transform.parent.gameObject.SetActive(false);
    }


    private void Update()
    {
        if (isEnding && creditTransform.anchoredPosition.y < 3055.17f)
        {
            creditTransform.anchoredPosition += new Vector2(0, scrollSpeed * Time.deltaTime);
        }
        
    }

    public void StartEnding()
    {
        SettingsStore.LoadSettings();

        double oldTime = SettingsStore.personalBest;

        if (oldTime == -1 || oldTime == 0.0)
        {
            SettingsStore.personalBest = TimerSingleton.instance.GetFinalTime();
        }
        else
        {
            if (!ReferenceSingleton.instance.cheatCode.IsCheated() && TimerSingleton.instance.GetFinalTime() < oldTime)
            {
                SettingsStore.personalBest = TimerSingleton.instance.GetFinalTime();
            }
        }

        
        SettingsStore.SaveSettings();

        pauseManager.isEnding = true;
        creditTransform.gameObject.SetActive(true);
        isEnding = true;
        creditAudio.Play();
        Invoke("EndingT", 32);
    }

    public void EndingT()
    {
        endBlackScreen.transform.parent.gameObject.SetActive(true);
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
