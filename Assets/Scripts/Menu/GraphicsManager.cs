using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

namespace Menu
{
   public class GraphicsManager : MonoBehaviour
   {
      public Slider brightnessSlider;
      public Light2D lighting;
      
      private void Start()
      {
         if (PlayerPrefs.HasKey("Brightness"))
         {
            PlayerPrefs.GetFloat("Brightness");
            SetSliders();
         }
         else
         {
            SetSliders();
         }
      }
    
      //For settings based on PlayerPrefs
      private void SetSliders()
      {
         brightnessSlider.value = PlayerPrefs.GetFloat("Brightness");
      }
     //For brightness slider
      public void OnChangeBrightness(float value)
      {
         lighting.intensity = brightnessSlider.value;
         PlayerPrefs.SetFloat("Brightness", brightnessSlider.value);
         PlayerPrefs.Save();
      }
   }
}
