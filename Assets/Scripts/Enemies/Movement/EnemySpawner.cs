using AudioScripts;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public void Spawn(GameObject randomEnemy, Transform target, AudioEventManager audioEventManager, float speedMultiplier, float healthMultiplier, int difficultyModifier, bool isRangedDifficulty)
    {
        GameObject cloneObject = Instantiate(randomEnemy, transform.position, Quaternion.identity);
        cloneObject.SetActive(true);
        NavMeshPathfinding nevMeshPathfinding = cloneObject.GetComponent<NavMeshPathfinding>();
        EnemyHealth enemyHealth = cloneObject.GetComponent<EnemyHealth>();
        EnemyAttack enemyAttack = cloneObject.GetComponent<EnemyAttack>();

        nevMeshPathfinding.TargetBase = target;
        nevMeshPathfinding.speedMultiplier = speedMultiplier;
        enemyAttack.audioEventManager = audioEventManager;
        enemyHealth.audioEventManager = audioEventManager;
        enemyHealth.healthPoints = (int)((float)enemyHealth.healthPoints * healthMultiplier);


        if (isRangedDifficulty)
        {
            float rangedDifficultyModifier = UnityEngine.Random.Range(1, difficultyModifier); // Add some random variation to the difficulty modifier for each enemy
            float rangedDifficultyModifier01 = rangedDifficultyModifier / 100f; // Convert to a 0-1 range for easier scaling

            enemyHealth.SetDifficultyModifier(rangedDifficultyModifier01);
            enemyAttack.SetDifficultyModifier(rangedDifficultyModifier01);
            nevMeshPathfinding.SetDifficultyModifier(difficultyModifier);
        }

    }
}
