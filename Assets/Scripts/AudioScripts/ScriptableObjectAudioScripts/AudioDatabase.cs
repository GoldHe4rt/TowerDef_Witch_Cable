using System.Collections.Generic;
using UnityEngine;
using Debug = System.Diagnostics.Debug;

namespace AudioScripts.ScriptableObjectAudioScripts
{
    [CreateAssetMenu(fileName = "AudioDatabase", menuName = "Scriptable Objects/AudioDatabase")]
    public class AudioDatabase : ScriptableObject
    {
        public static AudioDatabase Instance { get; private set; }
        public AudioEvent[] events;

        private Dictionary<string, AudioEvent> lookup;

        private void OnEnable()
        {
            Instance = this;
            Initialize();
        }

        private void Initialize()
        {
            if (lookup != null) return;

            lookup = new Dictionary<string, AudioEvent>();

            foreach (var e in events)
            {
                if (e == null || string.IsNullOrEmpty(e.eventId)) continue;

                lookup.TryAdd(e.eventId, e);
            }
        }

        public AudioEvent GetEvent(string id)
        {
            if (lookup == null)Initialize();
            Debug.Assert(lookup != null, nameof(lookup) + " != null");
            lookup.TryGetValue(id, out var audioEvent);
            return audioEvent;
        }
    }
}
