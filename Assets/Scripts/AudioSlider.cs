using System;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class AudioSlider : MonoBehaviour
{
   [SerializeField] private AudioMixer Mixer;
   [SerializeField] private AudioSource AudioSource;
   [SerializeField] private TextMeshProUGUI ValueText;
   [SerializeField] private AudioMixMode MixMode;


   private void Start()
   {
      Mixer.SetFloat("Volume", Mathf.Log10(PlayerPrefs.GetFloat("Volume", 1) * 20));
   }

   public void MasterVolume()
   {
      PlayerPrefs.SetFloat("Volume", 0);
      PlayerPrefs.Save();
   }

   public void OnChangeSlider(float Value)
   {
      //Shows slider value up to 4 decimals
      ValueText.SetText($"{Value:N4}");
      
      switch (MixMode)
      {
         case AudioMixMode.LinearAudioSourceVolume:
            AudioSource.volume = Value;
            break;
         case AudioMixMode.LinearMixerVolume:
            Mixer.SetFloat("Volume", (-80 + Value * 80));
            break;
         case AudioMixMode.LogarithmicMixerVolume:
            Mixer.SetFloat("Volume", Mathf.Log10(Value) * 20);
            break;
      }
   }
   
   public enum AudioMixMode
   {
      LinearAudioSourceVolume,
      LinearMixerVolume,
      LogarithmicMixerVolume
   }
}
