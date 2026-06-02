using UnityEngine;
using Random = UnityEngine.Random;

public class Waypoint : MonoBehaviour
{
    [SerializeField] public Waypoint[] waypoints;


    public Waypoint GetNextWaypoint()
    {
        if (waypoints.Length == 0)
        {
            return null;
        }

        int randomWaypointIndex = Random.Range(0, waypoints.Length);
        return waypoints[randomWaypointIndex];
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        foreach (Waypoint waypoint in waypoints)
        {
            if (waypoint != null)
            {
                Gizmos.DrawLine(transform.position, waypoint.transform.position);
            }
        }
        if (waypoints.Length == 0)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawSphere(transform.position, 3f);
        }
    }
}

