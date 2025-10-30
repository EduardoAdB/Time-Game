using UnityEngine;

public class KeyManager : MonoBehaviour
{
    public static KeyManager instance;

    public bool HasKey { get; private set; } = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // opcional: manter entre cenas
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Chame quando o jogador pegar a chave
    public void AcquireKey()
    {
        if (!HasKey)
        {
            HasKey = true;
            Debug.Log("KeyManager: chave ADQUIRIDA -> HasKey = true");
        }
        else
        {
            Debug.Log("KeyManager: AcquireKey chamado, mas HasKey já é true");
        }
    }

    // Opcional: resetar para debug/testes
    public void ResetKey()
    {
        HasKey = false;

        Debug.Log("KeyManager: chave resetada -> HasKey = false");
    }
}
