using UnityEngine;

namespace AudioScripts.ScriptableObjectAudioScripts
{
    [CreateAssetMenu(fileName = "AudioEvent", menuName = "Scriptable Objects/AudioEvent")]
    public class AudioEvent : ScriptableObject
    {
        [Header("Name")]
        public string eventId;
        
        [Header("Channel")]
        public AudioChannel audioChannel = AudioChannel.SFX; //default just to avoid forgetting to set one manually.
        
        [Header("Audio Clips")]
        public AudioClip[] clips;
        
        [Header("Volume & Pitch")]
        [Range(0.0001f, 1f)] public float baseVolume = 1f;
        [Range(-24f, 24f)] public float volumeOffsetDecibels;
        
        public bool randomizePitch;
        //Can be changed if it sounds bad
        [Range(0.5f, 2f)] public float minPitch = 0.9f;
        [Range(0.5f, 2f)] public float maxPitch = 1.1f;

        [Header("Loop setting")]
        public bool loop;

        [Header("Anti-sound spam settings")] 
        public float cooldown;
        public int maxInstances = 3;

        internal float lastPlayTime = -999f;
        [HideInInspector] public int currentInstances;

        public bool CanPlay() => Time.time >= lastPlayTime + cooldown;

        public AudioClip GetRandomClip()
        {
            if (clips == null || clips.Length == 0) return null;
            if (clips.Length == 1) return clips[0];
            int index = Random.Range(0, clips.Length);
            return clips[index];
        }

        public float GetRandomPitch() => !randomizePitch ? 1f : Random.Range(minPitch, maxPitch);
        
    }
}
