using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuPauseScript : MonoBehaviour
{
    public GameObject pauseMenuUI;
    private bool isPaused = false; // Variável para verificar se o jogo está pausado
    private Canvas pauseCanvas; // Variável para o componente Canvas

    public Button reiniciarJogoButton;  // Referência para o botão de reiniciar o jogo
    public Button voltarMenuPrincipalButton;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Se os botões estiverem configurados no Inspector, adicione os ouvintes para os cliques
        if (reiniciarJogoButton != null)
        {
            reiniciarJogoButton.onClick.AddListener(ReiniciarJogo); // Quando o botão for clicado, chama a função ReiniciarJogo
        }

        if (voltarMenuPrincipalButton != null)
        {
            voltarMenuPrincipalButton.onClick.AddListener(VolverMenuPrincipal); // Quando o botão for clicado, chama a função VoltarMenuPrincipal
        }

        // Tenta encontrar o Canvas do menu de pausa automaticamente
        if (pauseMenuUI == null)
        {
            pauseMenuUI = GameObject.Find("MenuPause"); // Substitua "MenuPause" pelo nome do seu Canvas
        }

        if (pauseMenuUI != null)
        {
            pauseCanvas = pauseMenuUI.GetComponent<Canvas>(); // Obtém o componente Canvas
            pauseCanvas.enabled = false; // Desativa o Canvas no início, mas mantém o GameObject ativo
        }
        else
        {
            Debug.LogError("MenuPause não encontrado!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Verifica se a tecla 'ESC' ou 'P' foi pressionada
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused)
            {
                ResumeGame(); // Retorna ao jogo
            }
            else
            {
                PauseGame(); // Pausa o jogo
            }
        }
    }

    // Função para pausar o jogo
    void PauseGame()
    {
        pauseCanvas.enabled = true; // Ativa o Canvas para mostrar o menu de pausa
        Time.timeScale = 0f; // Pausa o jogo
        isPaused = true; // Marca que o jogo está pausado
    }

    // Função para retomar o jogo
    void ResumeGame()
    {
        pauseCanvas.enabled = false; // Desativa o Canvas para esconder o menu de pausa
        Time.timeScale = 1f; // Retorna o tempo do jogo ao normal
        isPaused = false; // Marca que o jogo não está mais pausado
    }

    // Função para reiniciar o jogo completamente
    void ReiniciarJogo()
    {
        // Reinicia a cena atual
        StartCoroutine(ReloadScene());
    }

    // Função para reiniciar a cena de forma segura
    private IEnumerator ReloadScene()
    {
        // Espera o próximo quadro para garantir que o tempo seja restaurado
        yield return null;
        
        // Reseta o tempo do jogo, garantindo que tudo seja reiniciado corretamente
        Time.timeScale = 1f;
        
        // Carrega a cena novamente para reiniciar completamente
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Função para voltar ao menu principal
    void VolverMenuPrincipal()
    {
        SceneManager.LoadScene("MenuIniciar"); // Carrega a cena do menu principal
    }
}
