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
}

