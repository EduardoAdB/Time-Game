using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    static public QuizManager instance;
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
        public string respostaCorreta; // "A" ou "B"

        // 👇 Adicionados para versão em inglês
        [Header("Versão em Inglês")]
        public string textoPerguntaEN;
        public string alternativaAEN;
        public string alternativaBEN;
        public string alternativaCEN;
        public string respostaCorretaEN;

        public Pergunta instance;

        public void Awake()
        {
            instance = this;
        }
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

    private bool quizAtivo = false;
    public Collider2D ponteCollider; // referência da ponte bloqueada

    // 👇 Novo campo para botão de idioma
    [Header("Idioma")]
    public Button botaoIdioma;
    public TMP_Text textoBotaoIdioma;
    private bool emIngles = false; // controla o idioma atual

    void Start()
    {
        quizCanvas.SetActive(false);

        botaoA.interactable = false;
        botaoB.interactable = false;
        botaoC.interactable = false;

        botaoA.onClick.AddListener(() => VerificarResposta("A)Criar armadilhas no caminho dele"));
        botaoB.onClick.AddListener(() => VerificarResposta("B)Enviar mensageiros a cavalo"));
        botaoC.onClick.AddListener(() => VerificarResposta("C)Usar senhas fortes e autenticação de dois fatores"));

        // 👇 Listener do botão de idioma
        if (botaoIdioma != null)
            botaoIdioma.onClick.AddListener(TrocarIdioma);
    }

    public void AtivarQuiz(Collider2D ponte)
    {
        quizAtivo = true;
        ponteCollider = ponte;
        Time.timeScale = 0f;
        quizCanvas.SetActive(true);
        SortearPergunta();

        // Agora sim, habilita os botões
        botaoA.interactable = true;
        botaoB.interactable = true;
        botaoC.interactable = true;
    }

    public void SortearPergunta()
    {
        perguntaAtual = perguntas[Random.Range(0, perguntas.Count)];
        AtualizarTextoPergunta();
    }

    // 👇 Função auxiliar para atualizar texto conforme idioma
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

    // 👇 Função do botão de troca de idioma
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
            Debug.LogError("❌ Nenhuma pergunta atual definida! Certifique-se de chamar AtivarQuiz() antes de clicar em uma resposta.");
            return;
        }

        if (escolha == perguntaAtual.respostaCorreta)
        {
            Debug.Log("✅ Resposta correta! Ponte liberada.");

            if (ponteCollider != null)
                Destroy(ponteCollider.gameObject);

            Contador.instance.AvancarEra();
            FecharQuiz();
        }
        else
        {
            Debug.Log("❌ Resposta errada! Nova pergunta.");

            // ⚠️ Se ponteCollider estiver nulo, não tente reativar o quiz
            if (ponteCollider != null)
                AtivarQuiz(ponteCollider);
            else
                Debug.LogWarning("⚠️ ponteCollider está nulo, não foi possível reativar o quiz.");
        }
    }

    void FecharQuiz()
    {
        quizAtivo = false;
        quizCanvas.SetActive(false);
        Time.timeScale = 1f;

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
}
