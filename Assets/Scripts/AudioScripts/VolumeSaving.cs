using UnityEngine;

namespace AudioScripts
{
    public class VolumeSaving : MonoBehaviour
    {
        [SerializeField]private MixerVolumeApplier mixerVolumeApplier;
        [SerializeField] private string[] volumeKeys = { "MasterVolume", "MusicVolume", "SFXVolume", "UIVolume" };
        
        public void LoadVolumes()
        {
            foreach (string keys in volumeKeys)
            {
                float saved = PlayerPrefs.GetFloat(keys, 1f);
                float decibels = Mathf.Log10(Mathf.Clamp(saved, 0.0001f, 1f)) * 20f;
                mixerVolumeApplier.mixer.SetFloat(keys, decibels);
            }
        }

        public void SaveVolumes(int index, float value)
        {
            PlayerPrefs.SetFloat(volumeKeys[index], value);
            PlayerPrefs.Save();
        }
    }
}
