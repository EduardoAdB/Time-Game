using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    public static QuizManager instance;
    public void Awake()
    {
        instance = this;
    }

    [System.Serializable]
    public class Pergunta
    {
        public string textoPergunta;
        public string alternativaA;
        public string alternativaB;
        public string alternativaC;
        public string respostaCorreta;

        [Header("Versão em Inglês")]
        public string textoPerguntaEN;
        public string alternativaAEN;
        public string alternativaBEN;
        public string alternativaCEN;
        public string respostaCorretaEN;
    }

    [Header("Configurações do Quiz")]
    public List<Pergunta> perguntas = new List<Pergunta>();
    private Pergunta perguntaAtual;

    [Header("Referências UI")]
    public GameObject quizCanvas;
    public TMP_Text perguntaTexto;
    public Button botaoA;
    public Button botaoB;
    public Button botaoC;
    public TMP_Text textoBotaoA;
    public TMP_Text textoBotaoB;
    public TMP_Text textoBotaoC;

    [Header("Botão de Avançar Era")]
    public GameObject botaoAvancarEra;
    public TMP_Text textoBotaoAvancarEra;

    private bool quizAtivo = false;
    private bool emIngles = false;
    public Collider2D ponteCollider;

    [Header("Idioma")]
    public Button botaoIdioma;
    public TMP_Text textoBotaoIdioma;

    void Start()
    {
        quizCanvas.SetActive(false);

        botaoA.interactable = false;
        botaoB.interactable = false;
        botaoC.interactable = false;

        // listeners dos botões
        botaoA.onClick.AddListener(() => VerificarResposta("A)Criar armadilhas no caminho dele"));
        botaoB.onClick.AddListener(() => VerificarResposta("B)Enviar mensageiros a cavalo"));
        botaoC.onClick.AddListener(() => VerificarResposta("C)Usar senhas fortes e autenticação de dois fatores"));

        if (botaoIdioma != null)
            botaoIdioma.onClick.AddListener(TrocarIdioma);

        if (botaoAvancarEra != null)
            botaoAvancarEra.SetActive(false); // começa escondido
    }

    public void AtivarQuiz(Collider2D ponte)
    {
        quizAtivo = true;
        ponteCollider = ponte;
        Time.timeScale = 1f;
        quizCanvas.SetActive(true);
        SortearPergunta();

        botaoA.interactable = true;
        botaoB.interactable = true;
        botaoC.interactable = true;
    }

    public void SortearPergunta()
    {
        perguntaAtual = perguntas[Random.Range(0, perguntas.Count)];
        AtualizarTextoPergunta();

    }

    private void AtualizarTextoPergunta()
    {
        if (!emIngles)
        {
            perguntaTexto.text = perguntaAtual.textoPergunta;
            textoBotaoA.text = perguntaAtual.alternativaA;
            textoBotaoB.text = perguntaAtual.alternativaB;
            textoBotaoC.text = perguntaAtual.alternativaC;
        }
        else
        {
            perguntaTexto.text = perguntaAtual.textoPerguntaEN;
            textoBotaoA.text = perguntaAtual.alternativaAEN;
            textoBotaoB.text = perguntaAtual.alternativaBEN;
            textoBotaoC.text = perguntaAtual.alternativaCEN;
        }
    }

    public void TrocarIdioma()
    {
        emIngles = !emIngles;
        AtualizarTextoPergunta();

        if (textoBotaoIdioma != null)
            textoBotaoIdioma.text = emIngles ? "🇧🇷 PT-BR" : "🇺🇸 EN";
    }

    public void VerificarResposta(string escolha)
    {
        if (perguntaAtual == null)
        {
            Debug.LogError("❌ Nenhuma pergunta atual definida!");
            return;
        }

        if (escolha == perguntaAtual.respostaCorreta)
        {
            Debug.Log("✅ Resposta correta! Ponte liberada.");

            if (ponteCollider != null)
                Destroy(ponteCollider.gameObject);

            FecharQuiz();
            MostrarBotaoAvancarEra(); // 👈 aparece o botão só quando acertar
        }
        else
        {
            Debug.Log("❌ Resposta errada! Nova pergunta.");

            if (ponteCollider != null)
                AtivarQuiz(ponteCollider);
        }
    }

    void FecharQuiz()
    {
        quizAtivo = false;
        quizCanvas.SetActive(false);

        var jogador = GameObject.FindGameObjectWithTag("Player");
        if (jogador != null)
        {
            Rigidbody2D rb = jogador.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = Vector2.zero;
                rb.simulated = false;
                rb.simulated = true;
            }

            var mover = jogador.GetComponent<player>();
            if (mover != null)
                mover.enabled = true;
        }

        Debug.Log("✅ Quiz fechado e jogador destravado.");
    }

    // 🔹 Mostra o botão de avançar era
    void MostrarBotaoAvancarEra()
    {
        if (botaoAvancarEra != null)
        {
            botaoAvancarEra.SetActive(true);
            textoBotaoAvancarEra.text = emIngles ? "Next Era" : "Avançar Era  ";
            Debug.Log("🟢 Botão de avançar era ativado!");
        }
    }

    // 🔹 Chamado quando o jogador clica no botão
    public void BotaoAvancarEra()
    {
        if (Contador.instance != null)
        {
            Contador.instance.AvancarEra(); // muda a era
            Debug.Log("🏆 Era avançada com sucesso!");
        }

        if (botaoAvancarEra != null)
            botaoAvancarEra.SetActive(false); // esconde de novo
    }
}
