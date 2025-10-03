using UnityEngine;

public class PlayerColeta : MonoBehaviour
{
    public bool temChave = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Chave"))
        {
            temChave = true;
            Debug.Log("Pegou a chave!");
            Destroy(other.gameObject); // remove a chave da cena
        }
    }
}


