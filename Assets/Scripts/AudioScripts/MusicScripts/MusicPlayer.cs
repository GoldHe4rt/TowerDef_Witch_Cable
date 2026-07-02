using AudioScripts.ScriptableObjectAudioScripts;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AudioScripts
{
    public class MusicPlayer : MonoBehaviour
    {
        [SerializeField] private MusicProfile musicProfile;

        private int lastIndex = -1;

        private void Start()
        {
            switch (musicProfile.musicType)
            {
                case MusicType.MainMenu:
                case MusicType.Level:
                case MusicType.Credits:   
                    PlayRandomTrack();
                    break;
            }
        }

        private void PlayRandomTrack()
        {
            if (musicProfile.musicEventIds == null || musicProfile.musicEventIds.Length == 0) return;
            int index;

            if (musicProfile.shuffle)
                do { index = Random.Range(0, musicProfile.musicEventIds.Length); } 
                while (index == lastIndex && musicProfile.musicEventIds.Length > 1);
            else index = (lastIndex + 1) % musicProfile.musicEventIds.Length;

            lastIndex = index;
            
            AudioSystem.Play(musicProfile.musicEventIds[index]);
        }
    }
}
