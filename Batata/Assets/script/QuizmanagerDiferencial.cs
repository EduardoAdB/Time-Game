using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
  static  public QuizManager instance;
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
        public string respostaCorreta; // "A" ou "B"

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
    public TMP_Text textoBotaoA;
    public TMP_Text textoBotaoB;

    private bool quizAtivo = false;
    public Collider2D ponteCollider; // referência da ponte bloqueada

    void Start()
    {
        quizCanvas.SetActive(false);

        // Conectar botões
        botaoA.onClick.AddListener(() => VerificarResposta("A)Enviar mensageiros a cavalo."));
        botaoA.onClick.AddListener(() => VerificarResposta("A)Usar senhas fortes e autenticação de dois fatores."));
        botaoB.onClick.AddListener(() => VerificarResposta("B)Criar armadilhas no caminho dele."));
    }

    public void AtivarQuiz(Collider2D ponte)
    {
        quizAtivo = true;
        ponteCollider = ponte;

        // Pausar o jogo
        Time.timeScale = 0f;

        // Ativar Canvas
        quizCanvas.SetActive(true);

        // Selecionar pergunta aleatória
        SortearPergunta();
    }

   public void SortearPergunta()
    {
        perguntaAtual = perguntas[Random.Range(0, perguntas.Count)];

        // Atualizar UI
        perguntaTexto.text = perguntaAtual.textoPergunta;
        textoBotaoA.text = perguntaAtual.alternativaA;
        textoBotaoB.text = perguntaAtual.alternativaB;
    }

    void VerificarResposta(string escolha)
    {
        if (escolha == perguntaAtual.respostaCorreta)
        {
            Debug.Log("✅ Resposta correta! Ponte liberada.");

            // destrói bloqueio se tiver
            if (ponteCollider != null)
                Destroy(ponteCollider.gameObject);

            // 👉 avança a era no contador
            Contador.instance.AvancarEra();

            FecharQuiz();
        }
        else
        {
            Debug.Log("❌ Resposta errada! Nova pergunta.");
            AtivarQuiz(ponteCollider); // outra pergunta
        }
    }



    void FecharQuiz()
    {
        quizAtivo = false;
        quizCanvas.SetActive(false);
        Time.timeScale = 1f;
    }
}
