using UnityEngine;
using UnityEngine.Audio;

namespace AudioScripts
{
    public class PlaySound : MonoBehaviour
    {
        [SerializeField] private SoundType sound;
        private SoundManager soundManager;
        [SerializeField] private AudioSlider audioSlider;
        private PitchRandomizer pitchRandomizer;

        void Start()
        {
            soundManager.GetComponent<AudioSource>();
            audioSlider.GetComponent<AudioMixer>();
        }

        //Goes to PlayRandomSound function in SoundManager and plays a button sound, volume depends on respective slider value
        public void PlayButton()
        {
            //If slider value is 0.1f, mute the audio. Else play the audio.
            if (Mathf.Approximately(audioSlider.uiSlider.value, 0.1f))
            { Stop(); }
            else
            {pitchRandomizer.RandomPitch(); SoundManager.PlayRandomSound(SoundType.Button, volume: audioSlider.uiSlider.value); }
        }

        //Might need to the StateMachineFunction sound scripts instead.
        /*void PlaySFX()
    {
        //temporary stuff in here in case it is needed
        SoundManager.PlayRandomSound(sound, volume);
        if (audioSlider.sfxSlider.value == 0.1f)
        { Stop(); }
        else
        {pitchRandomizer.RandomPitch(); SoundManager.PlayRandomSound(sound, volume: audioSlider.sfxSlider.value); }
    }*/

        public void PlayMusic()
        {
            if (Mathf.Approximately(audioSlider.musicSlider.value, 0.1f))
            { Stop(); }
            else
            { SoundManager.PlayRandomSound(SoundType.Music, volume: audioSlider.musicSlider.value); }
        }

        private void Stop()
        { SoundManager.StopAudio(); }
    }
}
