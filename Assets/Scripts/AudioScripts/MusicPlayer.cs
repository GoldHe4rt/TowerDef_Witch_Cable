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
        { PlayRandomTrack(); }

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

        #region Play Specific Music
        public void PlayWinJingle()
        { if (!string.IsNullOrEmpty(musicProfile.winEventId)) AudioSystem.Play(musicProfile.winEventId); }
        
        /*public void PlayLoseJingle()
        { if (!string.IsNullOrEmpty(musicProfile.loseEventId)) AudioSystem.Play(musicProfile.loseEventId); }*/
        #endregion
        
    }
}
