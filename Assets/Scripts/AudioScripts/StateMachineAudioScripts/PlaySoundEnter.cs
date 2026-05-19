using UnityEngine;

namespace AudioScripts.StateMachineAudioScripts
{
    public class PlaySoundEnter : StateMachineBehaviour
    {
        [SerializeField] private string eventId;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        { if (!string.IsNullOrEmpty(eventId)) AudioSystem.Play(eventId); }
    }
}
