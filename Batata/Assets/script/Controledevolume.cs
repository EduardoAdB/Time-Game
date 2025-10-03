using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class ControleDeVolume : MonoBehaviour
{
    public AudioMixer mixer; // arraste seu AudioMixer no Inspector
    public Slider slider;    // arraste o Slider aqui

    void Start()
    {
        // Inicializa o slider com o valor atual do mixer
        float volume;
        mixer.GetFloat("VolumeMaster", out volume);
        slider.value = Mathf.Pow(10, volume / 20); // converte de dB para linear

        // Adiciona listener
        slider.onValueChanged.AddListener(SetVolume);
    }

 
    public void SetVolume(float valor)
    {
        // valor vai de 0 a 1 no slider
        mixer.SetFloat("VolumeMaster", Mathf.Log10(valor) * 20);
    }




}

