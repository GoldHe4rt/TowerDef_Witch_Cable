using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] public Waypoint waypoint;
    public void Spawn(GameObject randomEnemy, Transform target, int difficultyModifier, bool isRangedDifficulty)
    {
        GameObject cloneObject = Instantiate(randomEnemy, transform.position, Quaternion.identity);
        cloneObject.SetActive(true);
        NevMeshPathfinding nevMeshPathfinding = cloneObject.GetComponent<NevMeshPathfinding>();
        EnemyHealth enemyHealth = cloneObject.GetComponent<EnemyHealth>();
        EnemyAttack enemyAttack = cloneObject.GetComponent<EnemyAttack>();

        nevMeshPathfinding.TargetBase = target;

        if (isRangedDifficulty)
        {
            float rangedDifficultyModifier = UnityEngine.Random.Range(1, difficultyModifier); // Add some random variation to the difficulty modifier for each enemy
            float rangedDifficultyModifier01 = rangedDifficultyModifier / 100f; // Convert to a 0-1 range for easier scaling

            enemyHealth.SetDifficultyModifier(rangedDifficultyModifier01);
            enemyAttack.SetDifficultyModifier(rangedDifficultyModifier01);
            nevMeshPathfinding.SetDifficultyModifier(difficultyModifier);
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, waypoint.transform.position);
    }
}
