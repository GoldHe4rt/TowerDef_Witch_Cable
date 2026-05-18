using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AudioScripts
{
   public class AudioSlider : MonoBehaviour, IAudioSlider
   {
      [SerializeField] private AudioSource audioSource;

      public TMP_Text[] valueTexts;
      public Slider[] sliders;
      [SerializeField] private string[] volumeKeys = { "MasterVolume", "MusicVolume", "SFXVolume", "UIVolume" };

      [SerializeField] private VolumeSaving volumeSaving;
      [SerializeField] private MixerVolumeApplier mixerVolumeApplier;

      private void Start()
      {
         //Get the saved volumes
         volumeSaving.LoadVolumes();
         //Set the sliders accordingly
         SetSliders();
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

      #region Change Sliders
      public void OnChangeMasterSlider(float value) => OnChangeSlider(value, 0);
            public void OnChangeMusicSlider(float value) => OnChangeSlider(value, 1);
            public void OnChangeSfxSlider(float value) => OnChangeSlider(value, 2);
            public void OnChangeUiSlider(float value) => OnChangeSlider(value, 3);
      
            private void OnChangeSlider(float value, int index)
            {
               float snapped = Snap(value, 0.05f); //5%
               sliders[index].value = snapped;
      
               SetDisplayedText(index, snapped);
               volumeSaving.SaveVolumes(index, snapped);
               mixerVolumeApplier.ApplyMixerVolume(index, snapped);
            }
      #endregion
      
      private void SetDisplayedText(int index, float value)
      {
         //Show 0-100 instead of 0.1 to 1.
         int displayValue = Mathf.RoundToInt(value * 100f);
         if (value < 0.05f) displayValue = 0;
         valueTexts[index].SetText(displayValue + "%");
      }

      private static float Snap(float value, float step)
      {
         return Mathf.Round(value / step) * step;
      }

      #region SliderNameVolume -> slider[i].value
      //Volumes depend on the slider in the array
      public float MasterVolume => sliders[0].value;
      public float MusicVolume => sliders[1].value;
      public float SFXVolume => sliders[2].value;
      public float UIVolume => sliders[3].value;
      #endregion
   }
}
