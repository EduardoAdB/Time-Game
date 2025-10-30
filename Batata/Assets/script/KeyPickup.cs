using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    [Tooltip("Tag do jogador que coleta a chave")]
    public string playerTag = "Player";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            Debug.Log($"KeyPickup: player ({other.name}) entrou no trigger da chave ({gameObject.name})");

            if (KeyManager.instance != null)
            {
                KeyManager.instance.AcquireKey();

            }

            else
            {
                Debug.LogWarning("KeyPickup: KeyManager.instance é null! Lembre-se de adicionar um KeyManager na cena.");

            }

            Destroy(gameObject);
        }
    }
}
