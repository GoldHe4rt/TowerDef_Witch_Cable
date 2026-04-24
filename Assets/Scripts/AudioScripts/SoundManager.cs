using UnityEngine;
using System;


namespace AudioScripts
{
    [RequireComponent(typeof(AudioSource)), ExecuteInEditMode]
    public class SoundManager : MonoBehaviour
    {
        public enum SoundType
        {
            //Examples of type of sounds
            Magic,
            Land,
            Jump,
            Hurt,
            Footstep,
            Button,
            Music
        }


        public struct SoundList
        {
            public AudioClip[] Sounds { get => sounds; }
            [HideInInspector] public string name;
            [SerializeField] private AudioClip[] sounds;
            //aaa
        }

        [SerializeField] private SoundList[] soundList;
        private static SoundManager _instance;
        private AudioSource _audioSource;

        private void Awake()
        { _instance = this; }

        private void Start()
        { _audioSource = GetComponent<AudioSource>(); }

        public static void PlayRandomSound(SoundType sound, float volume)
        {
            AudioClip[] clips = _instance.soundList[(int)sound].Sounds;
            AudioClip randomClip = clips[UnityEngine.Random.Range(0, clips.Length)];
            _instance._audioSource.mute = false;
            _instance._audioSource.PlayOneShot(randomClip, volume);
        }

        public static void PlayMusic(SoundType sound, float volume)
        {
            AudioClip[] clips = _instance.soundList[(int)sound].Sounds;
            AudioClip musicClip = clips[Index.Start];
            _instance._audioSource.mute = false;
            _instance._audioSource.PlayOneShot(musicClip, volume);
        }

        public static void StopAudio()
        { _instance._audioSource.mute = true; }

#if UNITY_EDITOR
        private void OnEnable()
        {
            string[] names = Enum.GetNames(typeof(SoundType));
            Array.Resize(ref soundList, names.Length);
            for (int i = 0; i < soundList.Length; i++)
            { soundList[i].name = names[i]; }
        }
#endif
    }

}

//bbbb