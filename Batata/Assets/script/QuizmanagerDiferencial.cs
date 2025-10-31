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
        public string alternativaC;
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
    public Button botaoC;
    public TMP_Text textoBotaoA;
    public TMP_Text textoBotaoB;
    public TMP_Text textoBotaoC;

    private bool quizAtivo = false;
    public Collider2D ponteCollider; // referência da ponte bloqueada

    void Start()
    {
        quizCanvas.SetActive(false);

        botaoA.interactable = false;
        botaoB.interactable = false;
        botaoC.interactable = false;

        botaoA.onClick.AddListener(() => VerificarResposta("A)Criar armadilhas no caminho dele"));
        botaoB.onClick.AddListener(() => VerificarResposta("B)Enviar mensageiros a cavalo"));
        botaoC.onClick.AddListener(() => VerificarResposta("C)Usar senhas fortes e autenticação de dois fatores"));
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

        // Atualizar UI
        perguntaTexto.text = perguntaAtual.textoPergunta;
        textoBotaoA.text = perguntaAtual.alternativaA;
        textoBotaoB.text = perguntaAtual.alternativaB;
        textoBotaoC.text = perguntaAtual.alternativaC;
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

        // 🧩 Reativa física e movimento
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
