using UnityEngine;

public class QuitGame : MonoBehaviour
{
    public void SairDoJogo()
    {
        Debug.Log("Saindo do jogo...");
        Application.Quit();
    }
}

