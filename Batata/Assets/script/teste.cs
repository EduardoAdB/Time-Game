using UnityEngine;

public class Teste : MonoBehaviour
{
    [SerializeField] private int id; // ID configurável no inspetor
    private bool jogadorPerto = false; // Flag para saber se o player está no trigger

    private void Update()
    {
        if (jogadorPerto && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("🔘 Jogador pressionou E perto de: " + gameObject.name);
            CodigoSecreto.instance.AcionarInterruptor(id, gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jogadorPerto = true;
            Debug.Log("✅ Player encostou neste interruptor: " + gameObject.name);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jogadorPerto = false;
            Debug.Log("⏹️ Player saiu do interruptor: " + gameObject.name);
        }
    }
}
