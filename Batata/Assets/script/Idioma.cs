using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class IdiomaDicas : MonoBehaviour
{
    [Header("Referências de Texto das Dicas")]
    public TMP_Text[] dicasTextos; // arraste todos os Texts das dicas aqui no Inspetor

    [Header("Botão de Idioma")]
    public Button botaoIdioma;
    public TMP_Text textoBotaoIdioma;

    private bool emIngles = false;

    // Dicas em Português
    private string[] dicasPT = new string[]
    {
        "Dica:\nPara apertar nos quadros use Q para ver as dicas e Q para sair.",
        "Dica:\nPara aumentar o pulo use J e só pode usar 3 vezes esse super pulo. E recupera a energia depois de um tempo.",
        "Dica:\nPara desviar dos espinhos ou das pontes que somem e reaparecem tente pegar o time bom e parar o tempo usando F.",
        "Dica:\nDepois de toda era tem uma quest. Só passará se acertar.",
        "Dica:\nPara passar do puzzle das tochas tente clicar bem no fogo da tocha na ordem correta."
    };

    // Dicas em Inglês
    private string[] dicasEN = new string[]
    {
        "Hint:\nTo interact with panels use Q to see the hints and Q again to exit.",
        "Hint:\nTo increase your jump use J — you can use the super jump 3 times and it recharges after a while.",
        "Hint:\nTo avoid spikes or bridges that disappear and reappear, try to get the timing right and stop time using F.",
        "Hint:\nAfter each era there is a quest. You will only pass if you answer correctly.",
        "Hint:\nTo complete the torch puzzle, try clicking right on the fire of each torch in the correct order."
    };

    void Start()
    {
        // Garante que o botão tenha o listener
        if (botaoIdioma != null)
            botaoIdioma.onClick.AddListener(TrocarIdioma);

        AtualizarDicas();
    }

   public void TrocarIdioma()
    {
        emIngles = !emIngles;
        AtualizarDicas();


        if (textoBotaoIdioma != null)
            textoBotaoIdioma.text = emIngles ? " PT-BR" : " EN";
    }

    void AtualizarDicas()
    {
        if (dicasTextos.Length == 0) return;

        for (int i = 0; i < dicasTextos.Length; i++)
        {
            if (i < dicasTextos.Length)
            {

                dicasTextos[i].text = emIngles ? dicasEN[i] : dicasPT[i];
            }
        }
    }
}
