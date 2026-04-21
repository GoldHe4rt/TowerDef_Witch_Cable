using System;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

namespace AudioScripts
{
    public class PitchRandomizer : MonoBehaviour
    {
        [SerializeField] private SoundManager soundManager;
        [SerializeField] private float pitchVariation;
        private float basePitch;
        
        
        private void Start()
        {
            soundManager.GetComponent<AudioSource>();
            basePitch = soundManager.GetComponent<AudioSource>().pitch;
        }

        public void Randomize()
        {
            soundManager.GetComponent<AudioSource>().pitch = basePitch + Random.Range(-pitchVariation, pitchVariation);
        }
    }
}
