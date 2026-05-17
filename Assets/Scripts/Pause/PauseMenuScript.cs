using UnityEngine;

public class PauseMenuScript : MonoBehaviour
{

    private PauseManager pauseManager;
    private GameObject pauseManagerGO;

    [Header("Canvas")]
    
    [SerializeField] private GameObject mainCanva;
    [SerializeField] private GameObject settingsCanva;
    [SerializeField] private GameObject quitCanva;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GoToMainCanva();
        pauseManagerGO = GameObject.FindGameObjectWithTag("PauseManager");
        pauseManager = pauseManagerGO.GetComponent<PauseManager>();
    }
    private void OnEnable()
    {
        GoToMainCanva();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToMainCanva()
    {
        mainCanva.SetActive(true);
        settingsCanva.SetActive(false);
        quitCanva.SetActive(false);
    }

    public void ClickOnQuitButton()
    {
        mainCanva.SetActive(false);
        quitCanva.SetActive(true);
    }

    public void QuitGame()
    {
        Debug.Log("Game is quitting...");
        Application.Quit();
    }

    public void Resume()
    {
        pauseManager.Unpause(); // If failed : PauseManager must have the tag "PauseManager"

    }

    public void GoToSettings()
    {
        mainCanva.SetActive(false);
        settingsCanva.SetActive(true);
    }
}
