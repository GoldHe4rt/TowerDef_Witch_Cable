using UnityEngine;

namespace AudioScripts.ScriptableObjectAudioScripts
{
    public enum MusicType {MainMenu, Level, Credits}
    [CreateAssetMenu(fileName = "MusicProfile", menuName = "Scriptable Objects/MusicProfile")]
    
    public class MusicProfile : ScriptableObject
    {
        [Header("What type of scenes the music belongs in")]
        public MusicType musicType;
        [Header("Main Music Pool")]
        public string[] musicEventIds;
        [Header("Settings")]
        public bool shuffle = true;
    }
}
