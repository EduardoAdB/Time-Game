using System.Collections;
using UnityEngine;

public class SinoClickavel : MonoBehaviour
{
    public string corDoSino; // Ex: "Preto", "Verde", etc.
    private AudioSource audioSource;
    private SpriteRenderer sr;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown()
    {
        Debug.Log("🎯 Clique no sino: " + corDoSino);

        // 🔊 Tocar som do sino
        if (audioSource != null)
            audioSource.Play();

        // 🔁 Notificar o gerenciador
        SinoManager manager = FindObjectOfType<SinoManager>();
        if (manager != null)
        {
            manager.SinoTocado(this); // ✅ Envia o próprio sino
        }
        else
        {
            Debug.LogWarning("⚠️ SinoManager não encontrado na cena!");
        }
    }

    // 💚 Piscar verde (acerto)
    public IEnumerator PiscarAcerto()
    {
        if (sr != null)
        {
            Color original = sr.color;
            sr.color = Color.green;
            yield return new WaitForSeconds(0.3f);
            sr.color = original;
        }
    }

    // 🔴 Piscar vermelho (erro)
    public IEnumerator PiscarErro()
    {
        if (sr != null)
        {
            Color original = sr.color;
            sr.color = Color.red;
            yield return new WaitForSeconds(0.3f);
            sr.color = original;
        }
    }
}
