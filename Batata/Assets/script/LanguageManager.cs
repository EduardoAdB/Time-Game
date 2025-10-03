using System.Collections.Generic;
using UnityEngine;

public class LanguageManager : MonoBehaviour
{
    public static LanguageManager instance;

    // Idioma atual
    public string currentLanguage = "pt"; // "pt" = português, "en" = inglês

    // Dicionário com traduções
    private Dictionary<string, string> textsPT = new Dictionary<string, string>();
    private Dictionary<string, string> textsEN = new Dictionary<string, string>();

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // mantém entre cenas

            // 🔹 Carrega idioma salvo no PlayerPrefs
            currentLanguage = PlayerPrefs.GetString("Language", "pt");

            LoadTexts();
        }
        else
        {
            Destroy(gameObject); // garante que só exista 1
        }
    }

    void LoadTexts()
    {
        textsPT.Clear();
        textsEN.Clear();

        // Português
        textsPT.Add("Start", "Iniciar");
        textsPT.Add("Options", "Opções");
        textsPT.Add("Menu", "Menu");
        textsPT.Add("Sound", "Som");
        textsPT.Add("Language", "Linguagem");
        textsPT.Add("Help", "Ajuda");
        textsPT.Add("Power of time", "Poder do tempo");
        textsPT.Add("Credits", "Créditos");
        textsPT.Add("English", "Ingles");
        textsPT.Add("Portuguese", "Portugues");
        textsPT.Add("Resume", "Resumo");
        textsPT.Add("Quit", "sair");

        // Inglês
        textsEN.Add("Iniciar", "start");
        textsEN.Add("Opções", "Options");
        textsEN.Add("Menu", "menu");
        textsEN.Add("Som", "Sound");
        textsEN.Add("Linguagem", "Language");
        textsEN.Add("Ajuda", "Help");
        textsEN.Add("jogo do tempo", "Power of time");
        textsEN.Add("Créditos", "Credits");
        textsEN.Add("Ingles", "English");
        textsEN.Add("Portugues", "Portuguese");
        textsEN.Add("Resumo", "Resume");
        textsEN.Add("sair", "Quit");
    }

    public string GetText(string key)
    {
        if (currentLanguage == "pt" && textsPT.ContainsKey(key)) return textsPT[key];
        if (currentLanguage == "en" && textsEN.ContainsKey(key)) return textsEN[key];
        return key; // caso não encontre
    }

    public void ChangeLanguage(string lang)
    {
        currentLanguage = lang;

        // 🔹 Salva no PlayerPrefs
        PlayerPrefs.SetString("Language", lang);
        PlayerPrefs.Save();

        // Atualiza todos os textos da cena atual
        foreach (LocalizeText t in FindObjectsOfType<LocalizeText>())
        {
            t.UpdateText();
        }
    }
}
