using System;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace AudioScripts
{
   public class AudioSlider : MonoBehaviour
   {
      [Header("Audio")]
      public AudioMixer mixer;
      [SerializeField] private AudioSource audioSource;
      [SerializeField] private AudioMixMode mixMode;
      
      public TMP_Text[] valueTexts;

      [Header("Sliders")]
      public Slider[] sliders;

      private string[] volumeKeys = new[] { "MasterVolume" , "MusicVolume" , "SFXVolume" , "UIVolume"};
      
      private void Start()
      {
         //do we have saved volume player prefs?
         if (PlayerPrefs.HasKey("MasterVolume"))
         {
            //set the mixer volume levels based on saved player prefs
            mixer.SetFloat("MasterVolume", PlayerPrefs.GetFloat("MasterVolume"));
            mixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("MusicVolume"));
            mixer.SetFloat("SFXVolume", PlayerPrefs.GetFloat("SFXVolume"));
            mixer.SetFloat("UIVolume", PlayerPrefs.GetFloat("UIVolume"));
            SetSliders();
         }
         //otherwise just set the sliders
         else
         {
            SetSliders();
         }
      }

      //Set the slider values to saved volume settings
      private void SetSliders()
      {
         for (int i = 0; i < 4; i++)
         {
            sliders[i].value = PlayerPrefs.GetFloat(volumeKeys[i]);
            valueTexts[i].text = PlayerPrefs.GetFloat(volumeKeys[i]).ToString();
         }
      }

      //Save master volume across saves
      public void MasterVolume()
      {
         PlayerPrefs.SetFloat("MasterVolume", 0);
         PlayerPrefs.Save();
      }

      //Called when we update the sliders
      public void UpdateMasterVolume()
      {
         mixer.SetFloat("MasterVolume", sliders[0].value);
         PlayerPrefs.SetFloat("MasterVolume", sliders[0].value);
      }

      public void UpdateSfxVolume()
      {
         mixer.SetFloat("SFXVolume", sliders[1].value);
         PlayerPrefs.SetFloat("SFXVolume", sliders[1].value);
      }

      public void UpdateMusicVolume()
      {
         mixer.SetFloat("MusicVolume", sliders[2].value);
         PlayerPrefs.SetFloat("MusicVolume", sliders[2].value);
      }

      public void UpdateUIVolume()
      {
         mixer.SetFloat("UIVolume", sliders[3].value);
         PlayerPrefs.SetFloat("UIVolume", sliders[3].value);
      }

      public void OnChangeMasterSlider(float value)
      {
         OnChangeSlider(value : value, index: 0);
      }

      public void OnChangeMusicSlider(float value)
      {
         OnChangeSlider(value : value, index: 1);
      }

      public void OnChangeSfxSlider(float value)
      {
         OnChangeSlider(value : value, index: 2);
      }

      public void OnChangeUiSlider(float value)
      {
         OnChangeSlider(value : value, index: 3);
      }

      private void OnChangeSlider(float value, int index)
      {
         valueTexts[index].SetText($"{value}");
         if (mixMode == AudioMixMode.LogarithmicMixerVolume) 
            mixer.SetFloat(volumeKeys[index], Mathf.Log10(value) * 20);
      }

      private enum AudioMixMode
      { LogarithmicMixerVolume }
   }
}
