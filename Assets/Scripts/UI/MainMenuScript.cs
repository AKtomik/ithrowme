using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] private bool SkipIntro;
    [SerializeField] private InputActionAsset inputActions;
    [Header("Audio")]
    [SerializeField] private AudioSource mainMenuTheme;
    [SerializeField] private AudioSource cinematicTheme;
    [Header("UI")]
    [SerializeField] private Animator topAnimator;
    [SerializeField] private PauseMenuScript menuScript;
    [SerializeField] private RawImage cinematicBlack;
    [SerializeField] private TextMeshProUGUI m_Text;
    [SerializeField] private TextMeshProUGUI PBText;
    [SerializeField] private GameObject mainCanva;
    private InputAction click;
    private bool isStarted = false;
    private Scene scene;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        click = inputActions.FindAction("Pause");
        if (SkipIntro)
        {
            isStarted = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            cinematicBlack.gameObject.SetActive(true);
            StartCoroutine(StartGame());
            return;
        }
        mainMenuTheme.Play();
        cinematicBlack.gameObject.SetActive(false);
        

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 1;
        inputActions.FindActionMap("Player").Disable();
        inputActions.FindActionMap("UI").Enable();
        topAnimator.SetTrigger("One");
        Invoke("StartMenu", 14f);
        
    }


    private void StartMenu()
    {
        SettingsStore.LoadSettings();
        double time = SettingsStore.personalBest;

        if (time == -1 || time == 0.0)
        {
            PBText.gameObject.SetActive(false);
        }
        else
        {
            PBText.gameObject.SetActive(true);
            PBText.text = "Record : " + (Mathf.Round((float)time * 1000)) / 1000.0;
        }

        isStarted = true;
        topAnimator.SetTrigger("FadeOut");
        Invoke("DeleteAll", 1f);
    }
    private void DeleteAll()
    {
        menuScript.GoToMainCanva();

        Destroy(topAnimator.gameObject);

    }


    public void StartGameButton()
    {
        mainMenuTheme.Stop();
        cinematicBlack.gameObject.SetActive(true);
        mainCanva.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        yield return null;
        
        //Begin to load the Scene you specify
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("BaseLevel");
        //Don't let the Scene activate until you allow it to
        asyncOperation.allowSceneActivation = false;
        Debug.Log("Pro :" + asyncOperation.progress);
        //When the load is still in progress, output the Text and progress bar
        while (!asyncOperation.isDone)
        {
            
            //Output the current progress
            m_Text.text = "Chargement: " + (asyncOperation.progress * 100) + "%";

            // Check if the load has finished
            if (asyncOperation.progress >= 0.9f)
            {
                
                m_Text.text = "";
                cinematicTheme.Play();
                Debug.Log("WOAH");
                StartCoroutine(WaitForCinematicEnd(asyncOperation));
                break;
            }
            yield return null;

        }

    }

    private IEnumerator WaitForCinematicEnd(AsyncOperation asyncOperation)
    {
        yield return new WaitWhile(() => cinematicTheme.isPlaying);
        OnCinematicThemeFinished(asyncOperation);
    }

    private void OnCinematicThemeFinished(AsyncOperation asyncOperation)
    {
        asyncOperation.allowSceneActivation = true;
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1)
    }


    // Update is called once per frame
    void Update()
    {
        if (click.WasPressedThisFrame() && !isStarted)
        {
            CancelInvoke();
            StartMenu();
        }
    }
}
