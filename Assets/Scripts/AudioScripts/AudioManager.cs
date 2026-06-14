using AudioScripts.ScriptableObjectAudioScripts;
using UnityEngine;

namespace AudioScripts
{
    public class AudioManager : MonoBehaviour
    {
        //New stuff for WIP better sound system
        public static AudioManager Instance { get; private set; }

        [Header("Audio Sources")]
        public AudioSource musicSource;
        public AudioSource sfxSource;
        public AudioSource uiSource;

        [SerializeField] private AudioDatabase audioDatabase;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        public void PlaySFX(AudioEvent audioEvent)
        {
            var clip = audioEvent.GetRandomClip();
            if (clip == null) return;

            sfxSource.pitch = audioEvent.GetRandomPitch();
            sfxSource.PlayOneShot(clip, audioEvent.baseVolume);
        }

        public void PlayUI(AudioEvent audioEvent)
        {
            var clip = audioEvent.GetRandomClip();
            if (clip == null) return;

            uiSource.pitch = audioEvent.GetRandomPitch();
            uiSource.PlayOneShot(clip, audioEvent.baseVolume);
        }

        public void PlayMusic(AudioEvent audioEvent)
        {
            var clip = audioEvent.GetRandomClip();
            if (clip == null) return;

            musicSource.clip = clip;
            musicSource.loop = audioEvent.loop;
            musicSource.pitch = audioEvent.GetRandomPitch();
            musicSource.volume = audioEvent.baseVolume;
            musicSource.Play();
        }
    }
}
