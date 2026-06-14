using UnityEngine;

namespace AudioScripts.ScriptableObjectAudioScripts
{
    [CreateAssetMenu(fileName = "MusicProfile", menuName = "Scriptable Objects/MusicProfile")]
    public class MusicProfile : ScriptableObject
    {
        [Header("Level Music Pool")]
        public string[] musicEventIds;
        [Header("Win & Lose music")]
        public string winEventId;
        public string loseEventId;
        [Header("Settings")]
        public bool shuffle = true;
    }
}
