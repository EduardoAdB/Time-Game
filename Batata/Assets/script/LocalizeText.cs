using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class LocalizeText : MonoBehaviour
{
    private TextMeshProUGUI textComponent;
    public string key; // chave do texto (ex: "Start", "Options"...)

    void Awake()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
    }

    void OnEnable()
    {
        // 🔹 sempre que o objeto ativar, atualiza o texto
        UpdateText();
    }

    public void UpdateText()
    {
        if (textComponent == null) return;
        if (LanguageManager.instance == null) return;

        textComponent.text = LanguageManager.instance.GetText(key);
    }
}
