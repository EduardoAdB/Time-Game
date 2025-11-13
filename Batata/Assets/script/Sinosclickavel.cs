using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class SinoClickavel : MonoBehaviour
{
    public string corDoSino; // Ex: "Preto", "Verde", etc.
    private AudioSource audioSource;
    private SpriteRenderer sr;

    private bool jogadorPerto = false; // ✅ Detecta se o jogador está no collider

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // 🔑 Tocar o sino apenas quando o jogador estiver perto e apertar T
        if (jogadorPerto && Input.GetKeyDown(KeyCode.T))
        {
            TocarSino();
        }
    }

    private void TocarSino()
    {
        Debug.Log("🎯 Sino ativado: " + corDoSino);

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

    // 🧍‍♂️ Detecta quando o jogador entra no collider
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            jogadorPerto = true;
            Debug.Log("👣 Jogador perto do sino: " + corDoSino);
        }
    }

    // 🚶‍♂️ Detecta quando o jogador sai do collider
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            jogadorPerto = false;
            Debug.Log("🚪 Jogador saiu do sino: " + corDoSino);
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
