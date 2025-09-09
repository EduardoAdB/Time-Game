using UnityEngine;
using UnityEngine.SceneManagement;

public class PausarGame : MonoBehaviour
{
    [Header("Canvas do Pause")]
    public GameObject canvasPause;

    public GameObject canvasResume;


    private bool jogoPausado = false;

    private void Start()
    {
        if (canvasPause != null)
            canvasPause.SetActive(false); // Garante que começa desativado
    }

    // Esse método pode ser chamado pelo botão de pause
    public void PausarOuResumir()
    {
        if (jogoPausado)
        {
            // Resumir
            Time.timeScale = 1f;
            canvasPause.SetActive(false);
            jogoPausado = false;
        }
        else
        {
            // Pausar
            Time.timeScale = 0f;
            canvasPause.SetActive(true);
            jogoPausado = true;
            Contador.isTimeFrozen = true; // Garante que o tempo não está congelado
        }
    }

    // Botão de voltar ao menu
    public void VoltarMenu()
    {
        Time.timeScale = 1f; // Garante que o tempo volte ao normal
        SceneManager.LoadScene("MenuPrincipal"); // troque pelo nome certo da sua cena de menu
    }

    // Botão de sair do jogo
    public void SairDoJogo()
    {
        Debug.Log("Saindo do jogo...");
        Application.Quit();
    }
}

