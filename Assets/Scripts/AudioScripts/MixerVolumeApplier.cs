using UnityEngine;
using UnityEngine.Audio;

namespace AudioScripts
{
    public class MixerVolumeApplier : MonoBehaviour
    {
        public AudioMixer mixer;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioMixMode mixMode;
        [SerializeField]private AudioSlider audioSlider;
        [SerializeField] private string[] volumeKeys = { "MasterVolume", "MusicVolume", "SFXVolume", "UIVolume" };

        public void ApplyMixerVolume(int index, float value)
        {
            //Master volume effects the final volume
            float master = audioSlider.sliders[0].value;
            float finalValue = value * master;
            float mixerValue = finalValue;

            if (mixMode == AudioMixMode.LogarithmicMixerVolume) 
                mixerValue = Mathf.Log10(Mathf.Max(finalValue, 0.0001f)) * 20;
            mixer.SetFloat(volumeKeys[index], mixerValue);
        }
        
        private enum AudioMixMode
        { LogarithmicMixerVolume }
    }
}
