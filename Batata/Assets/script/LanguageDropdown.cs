using UnityEngine;
using UnityEngine.SceneManagement;

public class Language : MonoBehaviour
{
    public void Linguagem()
    {

        // Troca para a cena do jogo (coloque o nome exato da cena)
        SceneManager.LoadScene("Language");
    }

    public void Sair()
    {
        // Fecha o jogo (funciona só no executável)
        Application.Quit();
        Debug.Log("Jogo fechado."); // Para teste no editor
    }

    public void BacktoOptions()
    {
        SceneManager.LoadScene("Options");
    }
}
