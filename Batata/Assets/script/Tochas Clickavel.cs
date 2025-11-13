using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class TochaClickavel : MonoBehaviour
{
    private Tochas gerenciador;
    private string cor;
    private bool jogadorPerto = false;

    public void Definir(Tochas t, string c)
    {
        gerenciador = t;
        cor = c;
    }

    public string GetCor()
    {
        return cor;
    }

    void Update()
    {
        // Quando o jogador está perto e pressiona T
        if (jogadorPerto && Input.GetKeyDown(KeyCode.T))
        {
            if (gerenciador != null)
            {
                gerenciador.TentarClicar(this);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jogadorPerto = true;
            Debug.Log($"🕯️ Jogador perto da tocha {cor}");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jogadorPerto = false;
        }
    }

    public IEnumerator PiscarAcerto()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            Color originalColor = sr.color;
            sr.color = Color.green;
            yield return new WaitForSeconds(0.3f);
            sr.color = originalColor;
        }
    }

    public IEnumerator PiscarErro()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            Color originalColor = sr.color;
            sr.color = Color.red;
            yield return new WaitForSeconds(0.3f);
            sr.color = originalColor;
        }
    }
}
