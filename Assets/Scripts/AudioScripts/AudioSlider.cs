using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AudioScripts
{
   public class AudioSlider : MonoBehaviour
   {
      public Slider[] sliders;
      public TMP_Text[] valueTexts;
      
      private readonly string[] volumeKeys = { "MasterVolume", "MusicVolume", "SFXVolume", "UIVolume" };
      private AudioSliderVolumeBinder[] volumeBinders;

      //Auto-find all binders in children
      private void Awake()
      { volumeBinders = GetComponentsInChildren<AudioSliderVolumeBinder>(true); }

      private void Start()
      {
         //Set the sliders to the saved volumes
         for (int i = 0; i < sliders.Length; i++)
         {
            float saved = PlayerPrefs.GetFloat(volumeKeys[i], 1f);
            sliders[i].value = saved;
            UpdateText(i, saved);
         }
         
         //Tell volume binders to auto-detect their index and bind volume
         foreach (var volumeBinder in volumeBinders) volumeBinder.Bind(this);
      }

      #region Change Sliders
      public void OnChangeMasterSlider(float value) => OnChangeSlider(value, 0);
      public void OnChangeMusicSlider(float value) => OnChangeSlider(value, 1);
      public void OnChangeSfxSlider(float value) => OnChangeSlider(value, 2);
      public void OnChangeUiSlider(float value) => OnChangeSlider(value, 3);
      
      private void OnChangeSlider(float value, int index)
      {
         float snapped = Snap(value);
         sliders[index].value = snapped;
      
         PlayerPrefs.SetFloat(volumeKeys[index], snapped);
         PlayerPrefs.Save();
         
         UpdateText(index, snapped);
      }
      #endregion
      private static float Snap(float value) => Mathf.Round(value / 0.10f) * 0.10f;
      
      private void UpdateText(int index, float value)
      {
         int displayValue = Mathf.RoundToInt(value * 100f);
         if (value < 0.05f) displayValue = 0;
         valueTexts[index].SetText(displayValue + "%");
      }
   }
}
