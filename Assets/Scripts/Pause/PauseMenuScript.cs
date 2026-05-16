using UnityEngine;

public class PauseMenuScript : MonoBehaviour
{

    private PauseManager pauseManager;
    private GameObject pauseManagerGO;

    [Header("Canvas")]
    
    [SerializeField] private GameObject mainCanva;
    [SerializeField] private GameObject SettingsCanva;
    [SerializeField] private GameObject QuitCanva;



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
        SettingsCanva.SetActive(false);
        QuitCanva.SetActive(false);
    }

    public void ClickOnQuitButton()
    {
        mainCanva.SetActive(false);
        QuitCanva.SetActive(true);
    }

    public void QuitGame()
    {
        Debug.Log("Game is quitting...");
        Application.Quit();
    }

    public void Resume()
    {
        pauseManager.Unpause();

    }
}
