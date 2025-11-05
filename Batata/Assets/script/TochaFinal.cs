using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleUltimaTocha : MonoBehaviour
{
    [Header("Tochas pequenas (ordem correta)")]
    public GameObject[] tochasPequenas; // arraste na ordem certa no Inspector

    [Header("Tocha principal")]
    public GameObject tochaPrincipal; // tocha grande do centro

    [Header("Sprites das tochas")]
    public Sprite tochaApagada;
    public Sprite tochaAcesa;

    [Header("Efeitos")]
    public AudioSource somFinal;

    private int progresso = 0;
    private bool puzzleCompleto = false;

    void Start()
    {
        DesligarTodasTochas();
    }

    void Update()
    {
        if (puzzleCompleto) return;

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 posMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hit = Physics2D.OverlapPoint(posMouse);

            if (hit != null)
            {
                for (int i = 0; i < tochasPequenas.Length; i++)
                {
                    if (hit.gameObject == tochasPequenas[i])
                    {
                        if (i == progresso)
                        {
                            StartCoroutine(PiscarTochaCerta(tochasPequenas[i]));
                            progresso++;

                            if (progresso >= tochasPequenas.Length)
                                IniciarFinal();
                        }
                        else
                        {
                            DesligarTodasTochas();
                            progresso = 0;
                        }
                        break;
                    }
                }
            }
        }
    }

    IEnumerator PiscarTochaCerta(GameObject tocha)
    {
        SpriteRenderer sr = tocha.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.sprite = tochaAcesa;
            Color originalColor = sr.color;

            for (int i = 0; i < 3; i++)
            {
                sr.color = Color.green;
                yield return new WaitForSeconds(0.15f);
                sr.color = Color.white;
                yield return new WaitForSeconds(0.15f);
            }

            sr.color = originalColor;
        }
    }

    public void DesligarTodasTochas()
    {
        foreach (GameObject t in tochasPequenas)
        {
            SpriteRenderer sr = t.GetComponent<SpriteRenderer>();
            if (sr != null && tochaApagada != null)
                sr.sprite = tochaApagada;
        }

        if (tochaPrincipal != null)
        {
            SpriteRenderer sr = tochaPrincipal.GetComponent<SpriteRenderer>();
            if (sr != null && tochaApagada != null)
                sr.sprite = tochaApagada;
        }
    }

    void IniciarFinal()
    {
        puzzleCompleto = true;

        if (tochaPrincipal != null)
        {
            SpriteRenderer sr = tochaPrincipal.GetComponent<SpriteRenderer>();
            if (sr != null && tochaAcesa != null)
                sr.sprite = tochaAcesa;
        }

        if (somFinal != null)
            somFinal.Play();

        // Espera um pouco e carrega a próxima cena
        StartCoroutine(CarregarCenaFinal());
    }

    IEnumerator CarregarCenaFinal()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Créditos"); // 👉 troque para o nome da sua cena final
    }
}
