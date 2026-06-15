using System.Collections;
using System.Collections.Generic;
using AudioScripts.ScriptableObjectAudioScripts;
using UnityEngine;
using UnityEngine.Audio;

namespace AudioScripts
{
    public class AudioManager : MonoBehaviour
    {
        //New stuff for WIP better sound system
        public static AudioManager Instance { get; private set; }

        [Header("Audio Sources")]
        public AudioSource musicSource;
        public AudioSource uiSource;

        [Header("SFX audiosource pool settings")] 
        [SerializeField] private int initialPoolSize = 10; //might change this
        [SerializeField] private AudioMixerGroup sfxMixerGroup;

        private List<AudioSource> sfxPool;
        
        [SerializeField] private AudioDatabase audioDatabase;

        [Header("Global Anti-spam")] 
        [SerializeField] private float globalSfxCooldown = 0.05f;
        private float lastGlobalSfxTime = -999f;
        

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            CreateSFXPool();
        }

        #region Sound Effects

        private void CreateSFXPool()
        {
            sfxPool = new List<AudioSource>(initialPoolSize);

            for (int i = 0; i < initialPoolSize; i++)
            {
                AudioSource audioSource = gameObject.AddComponent<AudioSource>();
                audioSource.playOnAwake = false;
                audioSource.loop = false;
                audioSource.outputAudioMixerGroup = sfxMixerGroup;
                sfxPool.Add(audioSource);
            }
        }

        private AudioSource GetFreeSource()
        {
            //Look for a free AudioSource
            foreach (var audioSource in sfxPool)
                if (!audioSource.isPlaying)
                {
                    audioSource.Stop();
                    audioSource.clip = null;
                    return audioSource;
                }

            //Expand the pool if no free source is found. Need to test to see how this sounds.
            AudioSource newAudioSource = gameObject.AddComponent<AudioSource>();
            newAudioSource.playOnAwake = false;
            newAudioSource.loop = false;
            newAudioSource.outputAudioMixerGroup = sfxMixerGroup;
            
            sfxPool.Add(newAudioSource);

            return newAudioSource;
        }
        
        public void PlaySFX(AudioEvent audioEvent)
        {
            if (audioEvent == null) return;
            //Max instances at once
            if (audioEvent.currentInstances >= audioEvent.maxInstances) return; 
            //Per-event cooldown
            if (!audioEvent.CanPlay()) return;
            //Global cooldown
            if (Time.time < lastGlobalSfxTime + globalSfxCooldown) return;
            
        
            StartCoroutine(PlaySFXInstance(audioEvent));
        }
        
        private IEnumerator PlaySFXInstance(AudioEvent audioEvent)
        {
            audioEvent.currentInstances++;
            audioEvent.lastPlayTime = Time.time;
            lastGlobalSfxTime = Time.time;
            
            var clip = audioEvent.GetRandomClip();
            if (clip != null)
            {
                AudioSource audioSource = GetFreeSource();
                audioSource.pitch = audioEvent.GetRandomPitch();
                audioSource.volume = audioEvent.baseVolume;
                audioSource.clip = clip;
                audioSource.Play();
                yield return new WaitForSeconds(clip.length);
            }
        
            audioEvent.currentInstances--;
        }
        #endregion
        

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
