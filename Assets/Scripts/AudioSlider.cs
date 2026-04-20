using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class AudioSlider : MonoBehaviour
{
   [SerializeField] private AudioMixer mixer;
   [SerializeField] private AudioSource audioSource;
   [SerializeField] private TextMeshProUGUI valueText;
   [SerializeField] private AudioMixMode mixMode;

   private void Start()
   {
      mixer.SetFloat("Volume", Mathf.Log10(PlayerPrefs.GetFloat("Volume", 1) * 20));
   }

   
   //Save master volume across saves
   public void MasterVolume()
   {
      PlayerPrefs.SetFloat("Volume", 0);
      PlayerPrefs.Save();
   }

   public void OnChangeSlider(float value)
   {
      //Shows slider value up to 4 decimals
      valueText.SetText($"{value:N4}");
      
      switch (mixMode)
      {
         case AudioMixMode.LogarithmicMixerVolume:
            mixer.SetFloat("Volume", Mathf.Log10(value) * 20);
            break;
      }
   }
   
   public enum AudioMixMode
   { LogarithmicMixerVolume }
}
