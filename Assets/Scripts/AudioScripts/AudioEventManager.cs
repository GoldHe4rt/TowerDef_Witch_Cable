using UnityEngine;

namespace AudioScripts
{
    public class AudioEventManager : MonoBehaviour
    {
        [Header("Player Audio Events")]
        [SerializeField] private string playerAttackEventId = "Player_Attack";
        [SerializeField] private string playerDamageEventId = "Player_Damage";
        [SerializeField] private string playerDeathEventId = "Player_Death";
        [SerializeField] private string playerHealEventId = "Player_Heal";
        [SerializeField] private string playerDisconnectEventId = "Player_Disconnect";
        [SerializeField] private string playerJoinEventId = "Player_Join";
        [Header("Base Audio Events")]
        [SerializeField] private string baseDamageEventId = "Base_Damage";
        [SerializeField] private string baseDestroyEventId = "Base_Destroyed";
        [Header("Enemy Audio Events")]
        [SerializeField] private string enemyAttackEventId = "Enemy_Attack";
        [SerializeField] private string enemyDamageEventId = "Enemy_Damage";
        [SerializeField] private string enemyDefeatEventId = "Enemy_Defeat";
        [Header("Tower Audio Events")]
        [SerializeField] private string towerPlaceEventId = "Tower_Place";
        [SerializeField] private string towerRemoveEventId = "Tower_Remove";

        #region Player Sound Functions
        internal void PlayPlayerAttackSound() => AudioSystem.Play(playerAttackEventId);
        internal void PlayPlayerDamageSound() => AudioSystem.Play(playerDamageEventId);
        internal void PlayPlayerDeathSound() => AudioSystem.Play(playerDeathEventId);
        internal void PlayPlayerHealSound() => AudioSystem.Play(playerHealEventId);
        internal void PlayPlayerDisconnectSound() => AudioSystem.Play(playerDisconnectEventId);
        internal void PlayPlayerJoinSound() => AudioSystem.Play(playerJoinEventId);
        #endregion

        #region Base Sound Functions
        internal void PlayBaseDamageSound() => AudioSystem.Play(baseDamageEventId);
        internal void PlayBaseDestroySound() => AudioSystem.Play(baseDestroyEventId);
        #endregion

        #region Enemy Sound Functions
        internal void PlayEnemyAttackSound() => AudioSystem.Play(enemyAttackEventId);
        internal void PlayEnemyDamageSound() => AudioSystem.Play(enemyDamageEventId);
        internal void PlayEnemyDefeatSound() => AudioSystem.Play(enemyDefeatEventId);
        #endregion

        #region Tower Sound Functions
        internal void PlayTowerPlaceSound() => AudioSystem.Play(towerPlaceEventId);
        internal void PlayTowerRemoveSound() => AudioSystem.Play(towerRemoveEventId);
        #endregion
    }
}
