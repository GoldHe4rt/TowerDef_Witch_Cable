using UnityEngine;
using Random = UnityEngine.Random;

namespace AudioScripts
{
    public class PitchRandomizer : MonoBehaviour
    {
        private SoundManager soundManager;
        private float pitchVariation = 0.7f;
        private float basePitch;
        
        
        private void Start()
        {
            soundManager.GetComponent<AudioSource>();
            basePitch = soundManager.GetComponent<AudioSource>().pitch;
        }

        public void RandomPitch()
        {
            float randomPitch = Random.Range(1f - pitchVariation, 1f + pitchVariation);
            soundManager.GetComponent<AudioSource>().pitch = basePitch + randomPitch;
        }
    }
}
