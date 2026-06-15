using AudioScripts.VolumeSettings;
using UnityEngine;
using UnityEngine.UI;

namespace AudioScripts
{
    public class AudioSliderVolumeBinder : MonoBehaviour
    {
        public AudioChannel channel;

        private Slider slider;
        private int index;
        private AudioVolumeController volumeController;

        public void Bind(AudioSlider sliderGroup)
        {
            volumeController = FindFirstObjectByType<AudioVolumeController>();
            slider = GetComponent<Slider>();
            
            //Auto-detect the index of the slider
            for (int i = 0; i < sliderGroup.sliders.Length; i++)
                if (sliderGroup.sliders[i] == slider)
                {
                    index = i;
                    break;
                }
            
            //Apply initial volume
            volumeController.SetVolume(channel, slider.value);
            //Listen for slider changes
            slider.onValueChanged.AddListener(v => {volumeController.SetVolume(channel, v);});
        }
    }
}
