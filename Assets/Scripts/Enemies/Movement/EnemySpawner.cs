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
            float rangedDifficultyModifier01 = rangedDifficultyModifier / 100f; // Convert to a 0-1 range for easier scaling

            enemyHealth.SetDifficultyModifier(rangedDifficultyModifier01);
            enemyAttack.SetDifficultyModifier(rangedDifficultyModifier01);
            pathFinding.SetDifficultyModifier(difficultyModifier);
        }
        
    }

}
