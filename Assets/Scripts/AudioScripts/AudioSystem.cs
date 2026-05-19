using AudioScripts.ScriptableObjectAudioScripts;

namespace AudioScripts
{
    public static class AudioSystem
    {
        private static AudioDatabase DB => AudioDatabase.Instance;
        private static AudioManager AudioManager => AudioManager.Instance;

        public static void Play(string id)
        {
            var evt = DB.GetEvent(id);
            if (evt == null) return;

            switch (evt.audioChannel)
            {
                case AudioChannel.Music: AudioManager.PlayMusic(evt);
                    break;
                case AudioChannel.SFX: AudioManager.PlaySFX(evt);
                    break;
                case AudioChannel.UI: AudioManager.PlayUI(evt);
                    break;
            }
        }
    }
}
