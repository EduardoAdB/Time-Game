using UnityEngine;
using TMPro;

public class MudarIdiomaCreditos : MonoBehaviour
{
    [Header("Referência ao texto dos créditos")]
    [SerializeField] private TextMeshProUGUI textoCreditos;

    [Header("Referência ao texto do botão de sair")]
    [SerializeField] private TextMeshProUGUI textoBotaoSair;

    private bool emPortugues = true;

    // Texto dos créditos em português
    private string textoPT =
        "Fim da jornada. Mas os aprendizados permanecem.\n\n" +
        "Equipe de desenvolvimento Design/Programação:\n" +
        "Eduardo Augusto Bruns\n" +
        "Enzo Felipe Hostin\n\n" +
        "Artistas:\n" +
        "Lucas De Mello Lira\n" +
        "Gabriel Pedro Dos Passos\n\n" +
        "Redatores:\n" +
        "Nicolas DeBarba\n" +
        "Enzo Felipe Hostin";

    // Texto dos créditos em inglês
    private string textoEN =
        "End of the journey. But the lessons remain.\n\n" +
        "Development Team Design/Programming:\n" +
        "Eduardo Augusto Bruns\n" +
        "Enzo Felipe Hostin\n\n" +
        "Artists:\n" +
        "Lucas De Mello Lira\n" +
        "Gabriel Pedro Dos Passos\n\n" +
        "Writers:\n" +
        "Nicolas DeBarba\n" +
        "Enzo Felipe Hostin";

    // Texto do botão sair
    private string sairPT = "Sair do Jogo";
    private string sairEN = "Exit Game";

    // Chamado pelo botão de idioma
    public void TrocarIdioma()
    {
        emPortugues = !emPortugues;
        AtualizarTextos();
    }

    private void Start()
    {
        AtualizarTextos();
    }

    private void AtualizarTextos()
    {
        if (textoCreditos != null)
            textoCreditos.text = emPortugues ? textoPT : textoEN;

        if (textoBotaoSair != null)
            textoBotaoSair.text = emPortugues ? sairPT : sairEN;
    }

    // Chamado pelo botão de sair
    public void SairDoJogo()
    {
        Debug.Log("Saindo do jogo...");
        Application.Quit();

#if UNITY_EDITOR
        // Se estiver no editor, apenas para o Play Mode
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
