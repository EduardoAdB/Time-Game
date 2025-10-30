using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SinoManager : MonoBehaviour
{
    [Header("Referência dos Sinos")]
    public List<SinoClickavel> sinos;

    [Header("Tilemap a ser ativado após o sucesso")]
    public Tilemap tilemapParaAtivar;

    [Header("Ordem correta pré-definida")]
    public List<string> ordemCorreta = new List<string> { "Preto", "Verde", "Laranja", "Vermelho", "Amarelo" };

    private List<string> ordemDoJogador = new List<string>();
    private bool puzzleResolvido = false;

    void Start()
    {
        OcultarTilemap();
        Debug.Log("🧩 Ordem correta dos sinos: " + string.Join(" -> ", ordemCorreta));
    }

    void OcultarTilemap()
    {
        if (tilemapParaAtivar != null)
            tilemapParaAtivar.gameObject.SetActive(false);
    }

    void AtivarTilemap()
    {
        if (tilemapParaAtivar != null)
            tilemapParaAtivar.gameObject.SetActive(true);
    }

    // ✅ Método principal chamado pelo SinoClickavel
    public void SinoTocado(SinoClickavel sino)
    {
        if (puzzleResolvido)
        {
            Debug.Log("⛔ Puzzle já resolvido. Ignorando toque no sino: " + sino.corDoSino);
            return;
        }

        ordemDoJogador.Add(sino.corDoSino);
        int idx = ordemDoJogador.Count - 1;

        Debug.Log("🔔 Sino tocado: " + sino.corDoSino + " | Posição na sequência: " + ordemDoJogador.Count);

        // ⚠️ Proteção contra índice fora do limite
        if (idx >= ordemCorreta.Count)
        {
            Debug.LogWarning("❌ Tocou sinos a mais! Reiniciando sequência.");
            StartCoroutine(sino.PiscarErro());
            ResetarSequencia();
            return;
        }

        // ✅ Comparar com a ordem correta
        if (ordemCorreta[idx] == sino.corDoSino)
        {
            Debug.Log("✅ Cor correta! Esperado: " + ordemCorreta[idx]);
            StartCoroutine(sino.PiscarAcerto());

            // Se completou toda a ordem
            if (ordemDoJogador.Count == ordemCorreta.Count)
            {
                puzzleResolvido = true;
                Debug.Log("🎉 Puzzle dos sinos resolvido! Ativando tilemap...");
                AtivarTilemap();
                StartCoroutine(PiscarTodosVerde());
            }
        }
        else
        {
            Debug.Log("❌ Cor ERRADA! Tocou: " + sino.corDoSino + " | Esperado: " + ordemCorreta[idx]);
            StartCoroutine(sino.PiscarErro());
            ResetarSequencia();
        }
    }

    void ResetarSequencia()
    {
        ordemDoJogador.Clear();
        Debug.Log("🔄 Sequência reiniciada!");
    }

    // 🌟 Efeito visual ao completar (todos piscam verde)
    IEnumerator PiscarTodosVerde()
    {
        foreach (var sino in sinos)
        {
            if (sino != null)
                StartCoroutine(sino.PiscarAcerto());
        }
        yield return null;
    }
}
