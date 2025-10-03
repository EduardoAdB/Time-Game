using UnityEngine;

public class QuizTrigger : MonoBehaviour
{
    public QuizManager quizManager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("▶ Jogador entrou na área, abrindo quiz automaticamente.");
            quizManager.AtivarQuiz(this.GetComponent<Collider2D>());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("❌ Jogador saiu da área do quiz.");
        }
    }
}
