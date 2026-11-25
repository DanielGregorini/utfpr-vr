using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private Button startButton; // Referência para o botão

    void Start()
    {
        // Verifique se o botão foi atribuído, e adicione o evento de clique
        if (startButton != null)
        {
            startButton.onClick.AddListener(StartGame);
        }
        else
        {
            Debug.LogError("Botão não atribuído no Inspector");
        }
    }

    // Função que será chamada quando o botão for clicado
    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");  // Nome da sua cena de jogo
    }
}
