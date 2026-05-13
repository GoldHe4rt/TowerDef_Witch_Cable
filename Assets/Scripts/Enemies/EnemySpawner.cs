using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] public Waypoint waypoint;
    public void Spawn(GameObject randomEnemy)
    {
        GameObject cloneObject = Instantiate(randomEnemy, transform.position, Quaternion.identity);
        cloneObject.SetActive(true);
        cloneObject.GetComponent<PathFinding>().SetNewWaypoint(waypoint);
    }

}
