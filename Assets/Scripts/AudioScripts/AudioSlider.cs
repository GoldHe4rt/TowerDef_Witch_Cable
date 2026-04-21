using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSlider : MonoBehaviour
{
   public AudioMixer mixer;
   [SerializeField] private AudioSource audioSource;
   [SerializeField] private AudioMixMode mixMode;

   //Value texts
   public TextMeshProUGUI mastervalueText;
   public TextMeshProUGUI musicvalueText;
   public TextMeshProUGUI sfxvalueText;
   public TextMeshProUGUI uivalueText;

   //Sliders
   public Slider masterSlider;
   public Slider sfxSlider;
   public Slider musicSlider;
   public Slider uiSlider;

   private void Start()
   {
      //mixer.SetFloat("MasterVolume", Mathf.Log10(PlayerPrefs.GetFloat("MasterVolume", 1) * 20));
      //do we have saved volume player prefs?
      if (PlayerPrefs.HasKey("MasterVolume"))
      {
         //set the mixer volume levels based on saved player prefs
         mixer.SetFloat("MasterVolume", PlayerPrefs.GetFloat("MasterVolume"));
         mixer.SetFloat("SFXVolume", PlayerPrefs.GetFloat("SFXVolume"));
         mixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("MusicVolume"));
         mixer.SetFloat("UIVolume", PlayerPrefs.GetFloat("UIVolume"));
         SetSliders();
      }
      //otherwise just set the sliders
      else
      {
         SetSliders();
      }
   }

   //Set the slider values to be saved volume settings
   private void SetSliders()
   {
      masterSlider.value = PlayerPrefs.GetFloat("MasterVolume");
      sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");
      musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
      uiSlider.value = PlayerPrefs.GetFloat("UIVolume");
   }

   //Save master volume across saves
   public void MasterVolume()
   {
      PlayerPrefs.SetFloat("Volume", 0);
      PlayerPrefs.Save();
   }

   //Called when we update the sliders
   public void UpdateMasterVolume()
   {
      mixer.SetFloat("Volume", masterSlider.value);
      PlayerPrefs.SetFloat("Volume", masterSlider.value);
   }

   public void UpdateSfxVolume()
   {
      mixer.SetFloat("SFXVolume", sfxSlider.value);
      PlayerPrefs.SetFloat("SFXVolume", sfxSlider.value);
   }

   public void UpdateMusicVolume()
   {
      mixer.SetFloat("MusicVolume", musicSlider.value);
      PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
   }

   public void UpdateUIVolume()
   {
      mixer.SetFloat("UIVolume", uiSlider.value);
      PlayerPrefs.SetFloat("UIVolume", uiSlider.value);
   }

   public void OnChangeMasterSlider(float value)
   {
      //Shows slider value up to 4 decimals
      mastervalueText.SetText($"{value:N4}");
      if (mixMode == AudioMixMode.LogarithmicMixerVolume) mixer.SetFloat("MasterVolume", Mathf.Log10(value) * 20);
   }

   public void OnChangeMusicSlider(float value)
   {
      musicvalueText.SetText($"{value:N4}");
      if (mixMode == AudioMixMode.LogarithmicMixerVolume) mixer.SetFloat("MusicVolume", Mathf.Log10(value) * 20);
   }

   public void OnChangeSfxSlider(float value)
   {
      sfxvalueText.SetText($"{value:N4}");
      if (mixMode == AudioMixMode.LogarithmicMixerVolume) mixer.SetFloat("SFXVolume", Mathf.Log10(value) * 20);
   }

   public void OnChangeUiSlider(float value)
   {
      uivalueText.SetText($"{value:N4}");
      if (mixMode == AudioMixMode.LogarithmicMixerVolume) mixer.SetFloat("UIVolume", Mathf.Log10(value) * 20);
   }

   private enum AudioMixMode
   { LogarithmicMixerVolume }
}
