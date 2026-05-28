using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;


public class PathFinding : MonoBehaviour
{
    [SerializeField] public bool isActive = true;
    [SerializeField] private float minSpeed = 1f;
    [SerializeField] private float maxSpeed = 15f;
    [SerializeField] private float speed = 5f;
    private Waypoint waypoint;
    private int waypointsIndex;


    private Vector2 currentTarget;

    void Update()
    {
        if (!isActive) return;

        transform.position = Vector2.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);


        if (Vector2.Distance(transform.position, currentTarget) < 0.1f)
        {
            waypoint = waypoint.GetNextWaypoint();
            if (waypoint == null)
            {
                Destroy(gameObject);
                return;
            }
            SetNewWaypoint(waypoint);
        }
    }

    public void SetNewWaypoint(Waypoint newWayPoint)
    {
        waypoint = newWayPoint;
        Vector2 randomPos = waypoint.transform.position;
        randomPos.x += Random.Range(waypoint.transform.localScale.x / -2, waypoint.transform.localScale.x / 2);
        randomPos.y += Random.Range(waypoint.transform.localScale.y / -2, waypoint.transform.localScale.y / 2);

        currentTarget = randomPos;

    }

    internal void SetDifficultyModifier(float difficultyModifier)
    {
        float ClampedDifficultyModifier = Mathf.Clamp(difficultyModifier, 1, 100) / 100f; // Ensure the difficulty modifier is within range
        float currentMaxSpeed = maxSpeed * ClampedDifficultyModifier;
        speed = Random.Range(minSpeed, currentMaxSpeed);
    }
}
