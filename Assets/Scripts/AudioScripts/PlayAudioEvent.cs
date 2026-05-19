using UnityEngine;

namespace AudioScripts
{
    public class PlayAudioEvent : MonoBehaviour
    {
        [SerializeField] private string eventId;

        public void Play() => AudioSystem.Play(eventId);

        public void PlayMusic() => AudioSystem.Play(eventId);
    }
}
