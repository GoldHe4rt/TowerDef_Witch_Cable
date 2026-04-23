using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
   public class GraphicsManager : MonoBehaviour
   {
      [SerializeField] private GameObject graphicsMenu;
          
      public TextMeshProUGUI brightnessvalueText;
      public Slider brightnessSlider;
      public Light lighting;
          
    
      private void Start()
      {
         graphicsMenu.SetActive(false);
    
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
    
      //For graphics button
      public void ShowGraphicsMenu()
      { graphicsMenu.SetActive(true); }
    
      //For brightness slider
      public void OnChangeBrightness(float value)
      {
         brightnessvalueText.SetText($"{brightnessSlider.value}");
         lighting.intensity = brightnessSlider.value;
         PlayerPrefs.SetFloat("Brightness", brightnessSlider.value);
         PlayerPrefs.Save();
      }
   }
}
