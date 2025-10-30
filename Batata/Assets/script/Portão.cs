using System.Collections;
using UnityEngine;

public class CodigoSecreto : MonoBehaviour
{
    public static CodigoSecreto instance;

    void Awake()
    {
        instance = this;
    }

    [Header("Puzzle dos Interruptores")]
    public GameObject[] interruptores; // Lista dos interruptores
    public int[] ordemCorreta;         // Sequência correta
    public float tempoMaximo = 15f;

    private int indiceAtual = 0;
    private float tempoRestante;
    private GameObject interruptorAtual;

    [Header("Portão Final")]
    public GameObject portao;
    public Collider2D portaoCollider;
    public GameObject mensagemAbrirPortaoUI;

    private bool jogadorPertoDoPortao = false;

    [Header("Chave Única")]
    public GameObject chavePrefab;
    

    public bool resolvido = false;

    void Start()
    {
        tempoRestante = tempoMaximo;
        AtualizarPortao(false);

        if (mensagemAbrirPortaoUI != null)
            mensagemAbrirPortaoUI.SetActive(false);

        // Spawn da chave no mapa
        if (chavePrefab != null)
        {
            Instantiate(chavePrefab, new Vector3(-32.581f, -19.546f, 0), Quaternion.identity);
        }
    }

    void Update()
    {
        if (resolvido)
        {
            VerificarInteracaoPortao();
        }

        // Contador de tempo
        if (!resolvido && !Contador.isTimeFrozen)
        {
            tempoRestante -= Time.deltaTime;

            if (tempoRestante <= 0)
            {
                GameOver();
            }
        }

        // Ativar interruptor apenas se player estiver encostando e apertar E
        if (!resolvido && interruptorAtual != null && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("🔘 [Update] Jogador pressionou E perto de: " + interruptorAtual.name);
            Interruptor interruptor = interruptorAtual.GetComponent<Interruptor>();
            if (interruptor != null)
            {
                AcionarInterruptor(interruptor.id, interruptor.gameObject);
            }
            else
            {
                Debug.LogWarning("⚠ Nenhum componente 'Interruptor' encontrado em: " + interruptorAtual.name);
            }
        }
    }

    public void VerificarInteracaoPortao()
    {
        if (resolvido  && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("🚪 Jogador apertou E no portão.");
            AbrirPortaoFinal();
        }
    }

    public void AcionarInterruptor(int id, GameObject obj)
    {
        Debug.Log("🎮 Tentando acionar interruptor ID " + id + " | Esperado: " + ordemCorreta[indiceAtual]);

        if (resolvido) return;

        if (id == ordemCorreta[indiceAtual])
        {
            Debug.Log("✅ Interruptor correto: " + id);
            StartCoroutine(PiscarInterruptor(obj, Color.green)); // pisca verde
            indiceAtual++;

            if (indiceAtual >= ordemCorreta.Length)
            {
                PuzzleResolvido();
            }
        }
        else
        {
            Debug.Log("❌ Erro! Sequência incorreta. Reiniciando puzzle.");
            GameOver();
        }
    }

    public void PuzzleResolvido()
    {
        resolvido = true;
        AtualizarPortao(false);
        Debug.Log("🎉 Puzzle resolvido! Vá até o portão para abrir.");
    }

    void GameOver()
    {
        resolvido = false;
        indiceAtual = 0;
        tempoRestante = tempoMaximo;
        Debug.Log("⏳ Tempo esgotado ou erro na sequência. Puzzle reiniciado.");
    }

    void AtualizarPortao(bool abrir)
    {
        if (portao != null)
        {
            SpriteRenderer sr = portao.GetComponent<SpriteRenderer>();
            if (sr != null)
                sr.enabled = !abrir;
        }

        if (portaoCollider != null)
        {
            portaoCollider.enabled = !abrir;
            portaoCollider.isTrigger = abrir;
        }

        foreach (Transform child in portao.transform)
        {
            SpriteRenderer sr = child.GetComponent<SpriteRenderer>();
            if (sr != null)
                sr.enabled = !abrir;

            Collider2D col = child.GetComponent<Collider2D>();
            if (col != null)
            {
                col.enabled = !abrir;
                col.isTrigger = abrir;
            }
        }
    }

    void AbrirPortaoFinal()
    {
        Debug.Log("🚪 Portão final aberto!");
        AtualizarPortao(true);

        if (mensagemAbrirPortaoUI != null)
            mensagemAbrirPortaoUI.SetActive(false);
    }

    public void ResetarPuzzle()
    {
        indiceAtual = 0;
        tempoRestante = tempoMaximo;
        resolvido = false;
        AtualizarPortao(false);
        Debug.Log("🔄 Puzzle reiniciado.");
    }

    public float TempoRestante()
    {
        return tempoRestante;
    }

   

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("➡ [OnTriggerEnter2D] Colidiu com: " + other.name + " | Tag: " + other.tag);

        if (other.CompareTag("Interruptor"))
        {
            interruptorAtual = other.gameObject;

            // Deixa transparente indicando que pode interagir
            SpriteRenderer sr = interruptorAtual.GetComponent<SpriteRenderer>();
            if (sr != null)
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0.5f);

            Debug.Log("🔘 Jogador pode apertar E para acionar o interruptor: " + other.name);
        }
        else if (other.CompareTag("PortaoTrigger"))
        {
            jogadorPertoDoPortao = true;

            if (resolvido )
            {
                mensagemAbrirPortaoUI.SetActive(true);
                Debug.Log("📢 UI do portão ativada.");
            }
        }
        else if (other.CompareTag("Chave"))
        {
            Destroy(other.gameObject);
            
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("⬅ [OnTriggerExit2D] Saiu de: " + other.name + " | Tag: " + other.tag);

        if (other.CompareTag("Interruptor") && other.gameObject == interruptorAtual)
        {
            // Restaura a opacidade normal ao sair
            SpriteRenderer sr = interruptorAtual.GetComponent<SpriteRenderer>();
            if (sr != null)
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1f);

            Debug.Log("🔘 Saiu do interruptor: " + other.name);
            interruptorAtual = null;
        }
    }

    // Coroutine para piscar verde quando acertar
    IEnumerator PiscarInterruptor(GameObject obj, Color cor)
    {
        SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            Color original = sr.color;
            sr.color = cor;
            yield return new WaitForSeconds(0.3f);
            sr.color = new Color(original.r, original.g, original.b, 0.5f); // volta transparente enquanto player está colidindo
        }
    }
}
