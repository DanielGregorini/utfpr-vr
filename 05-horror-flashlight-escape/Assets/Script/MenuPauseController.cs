using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPauseController : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button mainMenuButton;

    private bool isPaused = false;

    void Start()
    {
        // Oculta o menu no início
        if (pauseMenu != null)
            pauseMenu.SetActive(false);

        // Conecta eventos aos botões
        if (resumeButton != null)
            resumeButton.onClick.AddListener(ResumeGame);

        if (quitButton != null)
            quitButton.onClick.AddListener(QuitGame);

        if (restartButton != null)
            restartButton.onClick.AddListener(RestartGame);

        if (mainMenuButton != null)
            mainMenuButton.onClick.AddListener(LoadMainMenu);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
                PauseGame();
            else
                ResumeGame();
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        if (pauseMenu != null)
            pauseMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        if (pauseMenu != null)
            pauseMenu.SetActive(false);
    }

    public void QuitGame()
    {
        Debug.Log("Saindo do jogo...");
        Application.Quit();

        #if UNITY_EDITOR
             UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
}
