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
                Gizmos.color = Color.red;
                Gizmos.DrawLine(transform.position, waypoint.transform.position);
                //Direction arrow Triangle
                Gizmos.color = Color.blue;
                Vector3 direction = (waypoint.transform.position - transform.position).normalized;
                Vector3 perpendicular = Vector3.Cross(direction, Vector3.forward).normalized;
                float arrowSize = 2f;
                float arrowDistance = 0.5f;
                Vector3 arrowTip = transform.position + direction * arrowSize;
                Vector3 arrowLeft = transform.position + direction * (arrowSize * 0.5f) + perpendicular * (arrowSize * 0.25f);
                Vector3 arrowRight = transform.position + direction * (arrowSize * 0.5f) - perpendicular * (arrowSize * 0.25f);
                Gizmos.DrawLine(transform.position + direction * arrowDistance, arrowTip);
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

