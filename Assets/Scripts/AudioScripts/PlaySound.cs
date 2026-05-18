using UnityEngine;
using UnityEngine.Audio;

namespace AudioScripts
{
    public class PlaySound : MonoBehaviour
    {
        [SerializeField] private SoundManager.SoundType sound;
        [SerializeField]private SoundManager soundManager;
        [SerializeField] private AudioSlider audioSlider;
        [SerializeField]private PitchRandomizer pitchRandomizer;

        void Start()
        {
            soundManager.GetComponent<AudioSource>();
            audioSlider.GetComponent<AudioMixer>();
        }

        //Goes to PlayRandomSound function in SoundManager and plays a button sound, volume depends on respective slider value
        public void PlayButton()
        {
            float master = audioSlider.MasterVolume;
            float ui = audioSlider.UIVolume;

            float finalVolume = ui * master;

            //Might need a change since sliders go down to 0.0001f.
            if (finalVolume <= 0.1f)
            {
                Stop();
                return;
            }

            pitchRandomizer.RandomPitch();
            SoundManager.PlayRandomSound(SoundManager.SoundType.Button, finalVolume);
        }

        //Might need to the StateMachineFunction sound scripts instead. Unsure.
        void PlaySFX()
        {
            float master = audioSlider.MasterVolume;
            float sfx = audioSlider.SFXVolume;

            float finalVolume = sfx * master;
            
            if (finalVolume <= 0.1f)
            {
                Stop();
                return;
            }

            SoundManager.PlayRandomSound(sound, finalVolume);
        }

        public void PlayMusic()
        {
            float master = audioSlider.MasterVolume;
            float music = audioSlider.MusicVolume;

            float finalVolume = music * master;

            if (finalVolume <= 0.1f)
            {
                Stop();
                return;
            }
            SoundManager.PlayRandomSound(SoundManager.SoundType.Music, finalVolume);
        }

        private void Stop()
        { SoundManager.StopAudio(); }
    }
}
