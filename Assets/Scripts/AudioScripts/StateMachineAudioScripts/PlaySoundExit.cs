using UnityEngine;

namespace AudioScripts.StateMachineAudioScripts
{
    public class PlaySoundExit : StateMachineBehaviour
    {
        [SerializeField] private SoundManager.SoundType sound;
        [SerializeField, Range(0, 1)] private float volume = 1f;

        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        { SoundManager.PlayRandomSound(sound, volume); }
    }
}
