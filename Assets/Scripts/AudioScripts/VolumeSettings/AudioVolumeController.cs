using UnityEngine;
using UnityEngine.Audio;

namespace AudioScripts.VolumeSettings
{
    public class AudioVolumeController : MonoBehaviour
    {

        [SerializeField] private AudioMixer mixer;

        private float master = 1f;
        private float music = 1f;
        private float sfx = 1f;
        private float ui = 1f;
        private const float MuteThreshold = 0.05f; //Mute at 5% volume

        public void SetVolume(AudioChannel channel, float value)
        {
            switch (channel)
            {
                case AudioChannel.Master: master = value;
                    break;
                case AudioChannel.Music: music = value;
                    break;
                case AudioChannel.SFX: sfx = value; 
                    break;
                case AudioChannel.UI: ui = value;
                    break;
            }
            
            ApplyVolume();
        }

        private void ApplyVolume()
        {
            float finalMusic = music * master;
            float finalSfx = sfx * master;
            float finalUI = ui * master;
            
            //Mute the audio when volume reaches mute threshold
            if (finalMusic < MuteThreshold) finalMusic = 0f;
            if (finalSfx < MuteThreshold) finalSfx = 0f;
            if (finalUI < MuteThreshold) finalUI = 0f;
            
            ApplyMixer("MasterVolume", master);
            ApplyMixer("MusicVolume", finalMusic);
            ApplyMixer("SFXVolume", finalSfx);
            ApplyMixer("UIVolume", finalUI);
        }

        private void ApplyMixer(string parameter, float linear)
        {
            float decibels = Mathf.Log10(Mathf.Clamp(linear, 0.0001f, 1f)) * 20f;
            mixer.SetFloat(parameter, decibels);
        }
    }
}
