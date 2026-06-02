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
                //Draw line to next waypoint
                Gizmos.color = Color.red;
                Vector3 direction = (waypoint.transform.position - transform.position).normalized;
                Vector3 perpendicular = Vector3.Cross(direction, Vector3.forward).normalized;
                Vector3 offset = perpendicular * 0.2f; // Adjust the offset for better visibility
                Vector3 startPoint = transform.position + direction + offset;
                Vector3 endPoint = waypoint.transform.position - direction + offset;
                Gizmos.DrawLine(transform.position, startPoint);
                Gizmos.DrawLine(startPoint, endPoint);
                Gizmos.DrawLine(waypoint.transform.position, endPoint);
                

                //Direction arrow
                Gizmos.color = Color.blue;
                float arrowSize = 2f;
                Vector3 arrowTip = startPoint + direction * arrowSize;
                Vector3 arrowLeft = startPoint + direction * (arrowSize * 0.5f) + perpendicular * (arrowSize * 0.25f);
                Vector3 arrowRight = startPoint + direction * (arrowSize * 0.5f) - perpendicular * (arrowSize * 0.25f);
                Gizmos.DrawLine(startPoint, arrowTip);
                Gizmos.DrawLine(arrowTip, arrowLeft);
                Gizmos.DrawLine(arrowTip, arrowRight);
            }
        }
        if (waypoints.Length == 0)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawSphere(transform.position, 3f);
        }
    }
}

