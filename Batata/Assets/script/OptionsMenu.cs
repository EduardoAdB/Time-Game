using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement; // Necessário para trocar de cena

public class OptionsMenu : MonoBehaviour
{
    // Start is called before the first frame update
   public void Options()
    {
        SceneManager.LoadScene("Options");
    }

    // Update is called once per frame
   public void SairOptions()
    {
        // Fecha o jogo (funciona só no executável)
        Application.Quit();
        Debug.Log("Jogo fechado."); // Para teste no editor
    }
}
