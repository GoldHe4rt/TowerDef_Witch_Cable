using UnityEngine;

namespace AudioScripts.StateMachineAudioScripts
{
    public class PlaySoundExit : StateMachineBehaviour
    {
        [SerializeField] private string eventId;

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        { if (!string.IsNullOrEmpty(eventId)) AudioSystem.Play(eventId); }
    }
}
