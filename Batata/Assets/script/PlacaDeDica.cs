using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlacaDeDica : MonoBehaviour
{
    [Header("Textos da dica")]
    [TextArea(2, 5)] public string mensagemDicaPT = "Use WASD para se mover e evite os inimigos!";
    [TextArea(2, 5)] public string mensagemDicaEN = "Use WASD to move and avoid enemies!";

    [Header("Referências da UI")]
    public GameObject painelDica;   // painel principal (preto)
    public TMP_Text textoDica;      // texto dentro do painel
    public Image papelFundo;        // imagem de papel no fundo
    public Button botaoIdioma;      // botão que alterna o idioma
    public TMP_Text textoBotaoIdioma; // texto do botão (ex: "EN"/"PT")

    private bool jogadorPerto = false;
    private bool dicaAtiva = false;
    private bool idiomaIngles = false; // idioma atual

    void Start()
    {
        if (painelDica != null)
            painelDica.SetActive(false);

        if (botaoIdioma != null)
            botaoIdioma.onClick.AddListener(TrocarIdioma);
    }

    void Update()
    {
        if (jogadorPerto && Input.GetKeyDown(KeyCode.Q))
        {
            if (!dicaAtiva)
                MostrarDica();
            else
                FecharDica();
        }
    }

    void MostrarDica()
    {
        if (painelDica != null)
        {
            painelDica.SetActive(true);
            AtualizarTexto();

            if (papelFundo != null)
                papelFundo.gameObject.SetActive(true);

            Time.timeScale = 0f;
            dicaAtiva = true;
        }
    }

    void FecharDica()
    {
        if (painelDica != null)
        {
            painelDica.SetActive(false);

            if (papelFundo != null)
                papelFundo.gameObject.SetActive(false);

            Time.timeScale = 1f;
            dicaAtiva = false;
        }
    }

   public void TrocarIdioma()
    {
        idiomaIngles = !idiomaIngles;
        AtualizarTexto();

        if (textoBotaoIdioma != null)
            textoBotaoIdioma.text = idiomaIngles ? "PT" : "EN";
    }

  public  void AtualizarTexto()
    {
        if (textoDica != null)
            textoDica.text = idiomaIngles ? mensagemDicaEN : mensagemDicaPT;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            jogadorPerto = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jogadorPerto = false;
            FecharDica();
        }
    }
}
