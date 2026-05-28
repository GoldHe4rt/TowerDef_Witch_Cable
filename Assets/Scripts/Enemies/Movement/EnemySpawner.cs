using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] public Waypoint waypoint;
    public void Spawn(GameObject randomEnemy, int difficultyModifier, bool isRangedDifficulty)
    {
        GameObject cloneObject = Instantiate(randomEnemy, transform.position, Quaternion.identity);
        cloneObject.SetActive(true);
        PathFinding pathFinding = cloneObject.GetComponent<PathFinding>();
        EnemyHealth enemyHealth = cloneObject.GetComponent<EnemyHealth>();
        EnemyAttack enemyAttack = cloneObject.GetComponent<EnemyAttack>();

        cloneObject.GetComponent<PathFinding>().SetNewWaypoint(waypoint);

        if (isRangedDifficulty)
        {
            float rangedDifficultyModifier = UnityEngine.Random.Range(1, difficultyModifier); // Add some random variation to the difficulty modifier for each enemy
            float clampedRangedDifficultyModifier = Mathf.Clamp(rangedDifficultyModifier, 1, 100); // Ensure the difficulty modifier is within range
            float clampedRangedDifficultyModifier01 = clampedRangedDifficultyModifier / 100f; // Convert to a 0-1 range for easier scaling

            enemyHealth.SetDifficultyModifier(clampedRangedDifficultyModifier01);
            enemyAttack.SetDifficultyModifier(clampedRangedDifficultyModifier01);
            pathFinding.SetDifficultyModifier(difficultyModifier);
        }
        
    }

}
