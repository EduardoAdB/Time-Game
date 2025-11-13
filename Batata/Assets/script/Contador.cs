using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Contador : MonoBehaviour
{
    int tick;
    public int hora;
    int minuto;
    int segundo;
    int dia;
    int tDia;
    int mes;
    public int ano;

    [SerializeField] TextMeshProUGUI tempo;
    [SerializeField] TextMeshProUGUI epoca;
    [SerializeField] TextMeshProUGUI botaoIdiomaTexto;

    int tickLog = 250;
    public static bool isTimeFrozen = false; // controla se o tempo do MUNDO está pausado
    public string era;
    bool isEnglish = false;

    [Header("Botão de Avançar Era")]
    [SerializeField] private Button botaoAvancarEra;
    [SerializeField] private TMP_Text textoBotaoAvancarEra;
    private bool podeAvancar = false;

    #region Singleton
    public static Contador instance;
    private void Awake() { instance = this; }
    #endregion

    void Start()
    {
        segundo = 0;
        minuto = 0;
        hora = 0;
        tick = 0;
        dia = 0;
        mes = 1;
        ano = 1;
        era = "PréHistórica";
        AtualizarTextos();

        if (botaoAvancarEra != null)
        {
            botaoAvancarEra.gameObject.SetActive(false);
            botaoAvancarEra.onClick.AddListener(AoClicarAvancarEra);
        }
    }

    void Update()
    {
        // 🔹 Alternar pausa seletiva
        if (Input.GetKeyDown(KeyCode.F))
        {
            isTimeFrozen = !isTimeFrozen;
            Debug.Log(isTimeFrozen ? "⏸️ Tempo parado (Moai pode se mover)" : "▶️ Tempo retomado");
        }

        // 🔹 Alternar velocidade do tempo (só afeta os que usam o contador)
        if (Input.GetKeyDown(KeyCode.G))
        {
            tickLog = (tickLog == 250) ? 25 : 250;
            Debug.Log("⚡ Velocidade do tempo: " + (tickLog == 25 ? "Rápida" : "Normal"));
        }

        // 🔹 Atualiza o tempo apenas se o tempo não estiver parado
        if (!isTimeFrozen)
        {
            AtualizarTempo();
            AtualizarTextos();
        }
    }

    public void AtivarBotaoAvancarEra()
    {
        if (botaoAvancarEra != null)
        {
            podeAvancar = true;
            botaoAvancarEra.gameObject.SetActive(true);
            textoBotaoAvancarEra.text = isEnglish ? "Advance Era" : "Avançar Era";
            Debug.Log("🟢 Botão de avançar era ativado!");
        }
    }

    private void AoClicarAvancarEra()
    {
        if (!podeAvancar) return;

        AvancarEra();
        podeAvancar = false;
        botaoAvancarEra.gameObject.SetActive(false);
    }

    void AtualizarTempo()
    {
        if (tick < tickLog) tick++;
        else { segundo++; tick = 0; }

        if (segundo == 1) { minuto++; segundo = 0; }
        if (minuto == 1) { hora++; minuto = 0; }
        if (hora == 1) { dia++; tDia++; hora = 0; }

        int diasNoMes = GetDiasNoMes(mes, ano);
        if (dia > diasNoMes) { dia = 1; mes++; }
        if (mes > 12) { mes = 1; ano++; }
    }

    public void TrocarIdioma()
    {
        isEnglish = !isEnglish;
        botaoIdiomaTexto.text = isEnglish ? "Português" : "English";
        AtualizarTextos();

        if (botaoAvancarEra != null && botaoAvancarEra.gameObject.activeSelf)
            textoBotaoAvancarEra.text = isEnglish ? "Advance Era" : "Avançar Era";
    }

    void AtualizarTextos()
    {
        string formattedAno = ano.ToString("D2");
        string formattedMes = mes.ToString("D2");
        string formattedDia = dia.ToString("D2");

        string eraTraduzida = era;

        if (isEnglish)
        {
            switch (era)
            {
                case "PréHistórica": eraTraduzida = "Prehistoric"; break;
                case "Medieval": eraTraduzida = "Medieval"; break;
                case "Moderna": eraTraduzida = "Modern"; break;
            }

            tempo.text = $"Time: {formattedAno}:{formattedMes}:{formattedDia}";
            epoca.text = $"Era: {eraTraduzida}";
        }
        else
        {
            tempo.text = $"O tempo é {formattedAno}:{formattedMes}:{formattedDia}";
            epoca.text = $"Era {era}";
        }
    }

    int GetDiasNoMes(int mes, int ano)
    {
        switch (mes)
        {
            case 1: case 3: case 5: case 7: case 8: case 10: case 12: return 31;
            case 4: case 6: case 9: case 11: return 30;
            case 2: return IsLeapYear(ano) ? 29 : 28;
            default: throw new System.ArgumentOutOfRangeException("Mês inválido");
        }
    }

    bool IsLeapYear(int ano)
    {
        return (ano % 4 == 0) && (ano % 100 != 0 || ano % 400 == 0);
    }

    public void AvancarEra()
    {
        if (era == "PréHistórica") era = "Medieval";
        else if (era == "Medieval") era = "Moderna";
        else if (era == "Moderna") Debug.Log("🏆 Todas as eras concluídas!");

        AtualizarTextos();
        Debug.Log("🔄 Era avançada para: " + era);
    }
}
