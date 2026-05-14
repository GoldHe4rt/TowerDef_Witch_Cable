using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace AudioScripts
{
   public class AudioSlider : MonoBehaviour, IAudioSlider
   {
      //Need to find ways to shorten the script to make it easier to go through when problems arise.
      //This version is longer but should work better than previous versions.
      public AudioMixer mixer;
      [SerializeField] private AudioSource audioSource;
      [SerializeField] private AudioMixMode mixMode;
      
      public TMP_Text[] valueTexts;
      public Slider[] sliders;
      [SerializeField]
      private string[] volumeKeys = { "MasterVolume" , "MusicVolume" , "SFXVolume" , "UIVolume"};
      
      private void Start()
      {
            LoadVolumes();
            SetSliders();
      }

      //Get the saved volumes
      private void LoadVolumes()
      {
         foreach (string keys in volumeKeys)
         {
            float saved = PlayerPrefs.GetFloat(keys, 1f);
            float dB = Mathf.Log10(Mathf.Clamp(saved, 0.0001f, 1f)) * 20f;
            mixer.SetFloat(keys, dB);
         }
      }
      private void SetSliders()
      {
         for (int i = 0; i < sliders.Length; i++)
         {
            float values = PlayerPrefs.GetFloat(volumeKeys[i], 1f);
            sliders[i].value = values;
            SetDisplayedText(i, values);
         }
      }
      
      public void OnChangeMasterSlider(float value)=> OnChangeSlider(value,0);
      public void OnChangeSfxSlider(float value)=> OnChangeSlider(value,1);
      public void OnChangeMusicSlider(float value)=> OnChangeSlider(value,2);
      public void OnChangeUiSlider(float value) => OnChangeSlider(value,3);

      private void OnChangeSlider(float value, int index)
      {
         float snapped = Snap(value, 0.05f); //5%
         sliders[index].value = snapped;
         
         SetDisplayedText(index, snapped);
         SaveValue(index, snapped);
         ApplyMixerVolume(index, snapped);
      }
      
      private void SetDisplayedText(int index, float value)
      {
         //Show 0-100 instead of 0.1 to 1 without changing sliders to from 0-100
         int displayValue = Mathf.RoundToInt(value * 100f);
         if (value < 0.05f) displayValue = 0;
         valueTexts[index].SetText(displayValue + "%");
      }

      //Save volume values across saves
      private void SaveValue(int index, float value)
      {
         PlayerPrefs.SetFloat(volumeKeys[index], value);
         PlayerPrefs.Save();
      }

      //Make sure the master volume affects the other volumes
      private void ApplyMixerVolume(int index, float value)
      {
         float master = sliders[0].value;
         float finalValue = value * master;

         float mixerValue = finalValue;
         if (mixMode == AudioMixMode.LogarithmicMixerVolume)
            mixerValue = Mathf.Log10(Mathf.Max(finalValue, 0.0001f)) * 20;

         mixer.SetFloat(volumeKeys[index], mixerValue);
      }

      private float Snap(float value, float step)
      {
         return Mathf.Round(value / step) * step;
      }

      //Volumes depend on the value of the slider in the array
      public float MasterVolume => sliders[0].value; 
      public float SFXVolume => sliders[1].value;
      public float MusicVolume => sliders[2].value;
      public float UIVolume => sliders[3].value;

      private enum AudioMixMode
      { LogarithmicMixerVolume }
   }
}
