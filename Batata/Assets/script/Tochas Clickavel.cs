using System.Collections;
using UnityEngine;

public class TochaClickavel : MonoBehaviour
{
    private Tochas gerenciador;
    private string cor;

    public void Definir(Tochas t, string c)
    {
        gerenciador = t;
        cor = c;
    }

    public string GetCor() // ← ESSA PARTE É IMPORTANTE
    {
        return cor;
    }

    private void OnMouseDown()
    {
        if (gerenciador != null)
        {
            gerenciador.TentarClicar(this); // 🔹 Passamos a própria tocha para o gerenciador
        }
    }

    public IEnumerator PiscarAcerto()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            Color originalColor = sr.color;
            sr.color = Color.green; // 💚 Muda para verde
            yield return new WaitForSeconds(0.3f);
            sr.color = originalColor; // volta à cor original
        }
    }
    public IEnumerator PiscarErro()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            Color originalColor = sr.color;
            sr.color = Color.red; // 🔴 Pisca vermelho
            yield return new WaitForSeconds(0.3f);
            sr.color = originalColor;
        }
    }

}
