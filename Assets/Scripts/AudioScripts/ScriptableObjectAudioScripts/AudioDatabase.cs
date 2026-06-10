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

        private Dictionary<string, AudioEvent> lookupEventName;

        private void OnEnable()
        {
            Instance = this;
            Initialize();
        }

        private void Initialize()
        {
            if (lookupEventName != null) return;

            lookupEventName = new Dictionary<string, AudioEvent>();

            foreach (AudioEvent audioEvent in events)
            {
                if (audioEvent == null || string.IsNullOrEmpty(audioEvent.eventId)) continue;

                lookupEventName.TryAdd(audioEvent.eventId, audioEvent);
            }
        }

        public AudioEvent GetEvent(string id)
        {
            if (lookupEventName == null)Initialize();
            Debug.Assert(lookupEventName != null, nameof(lookupEventName) + " != null");
            lookupEventName.TryGetValue(id, out var audioEvent);
            return audioEvent;
        }
    }
}
