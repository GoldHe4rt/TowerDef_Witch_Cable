using UnityEngine;

namespace AudioScripts
{
    public class LinesToUse : MonoBehaviour
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
        private void PlayPlayerAttackSound() => AudioSystem.Play(playerAttackEventId);
        private void PlayPlayerDamageSound() => AudioSystem.Play(playerDamageEventId);
        private void PlayPlayerDeathSound() => AudioSystem.Play(playerDeathEventId);
        private void PlayPlayerHealSound() => AudioSystem.Play(playerHealEventId);
        private void PlayPlayerDisconnectSound() => AudioSystem.Play(playerDisconnectEventId);
        private void PlayPlayerJoinSound() => AudioSystem.Play(playerJoinEventId);
        #endregion

        #region Base Sound Functions
        private void PlayBaseDamageSound() => AudioSystem.Play(baseDamageEventId);
        private void PlayBaseDestroySound() => AudioSystem.Play(baseDestroyEventId);
        #endregion

        #region Enemy Sound Functions
        private void PlayEnemyAttackSound() => AudioSystem.Play(enemyAttackEventId);
        private void PlayEnemyDamageSound() => AudioSystem.Play(enemyDamageEventId);
        private void PlayEnemyDefeatSound() => AudioSystem.Play(enemyDefeatEventId);
        #endregion

        #region Tower Sound Functions
        private void PlayTowerPlaceSound() => AudioSystem.Play(towerPlaceEventId);
        private void PlayTowerRemoveSound() => AudioSystem.Play(towerRemoveEventId);
        #endregion
    }
}
