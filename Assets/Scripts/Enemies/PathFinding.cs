using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;


public class PathFinding : MonoBehaviour
{
    [SerializeField] public bool isActive = true;
    public float speed;
    private Waypoint waypoint;
    private int waypointsIndex;
<<<<<<< Updated upstream

    void Start()
    {
        if (!isActive) return;
        Wpoints = GameObject.FindGameObjectWithTag("Waypoints").GetComponent<Waypoints>();

    }

    void Update()
    {
        if (!isActive) return;
        transform.position = Vector2.MoveTowards(transform.position, Wpoints.waypoints[waypointsIndex].position, speed * Time.deltaTime);
=======
    private Vector2 currentTarget;

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);
>>>>>>> Stashed changes

        Vector3 dir = waypoint.transform.position - transform.position;
        float angle = Mathf.Atan(dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

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
}
