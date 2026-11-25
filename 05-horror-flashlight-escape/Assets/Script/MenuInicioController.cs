using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuInicioController : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button quitButton;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
        if (startButton != null)
            startButton.onClick.AddListener(StartGame);

        if (quitButton != null)
            quitButton.onClick.AddListener(QuitGame);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1); // Carrega a cena de índice 1
    }

    public void QuitGame()
    {
        Debug.Log("Saindo do jogo...");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
