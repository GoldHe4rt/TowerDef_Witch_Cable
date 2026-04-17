using System.Runtime.CompilerServices;
using UnityEngine;


public class PathFinding : MonoBehaviour
{
    public float speed;
    private Waypoints Wpoints;

    private int waypointsIndex;

    void Start()
    {
        Wpoints = GameObject.FindGameObjectWithTag("Waypoints").GetComponent<Waypoints>();

    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, Wpoints.waypoints[waypointsIndex].position, speed * Time.deltaTime);

        Vector3 dir = Wpoints.waypoints[waypointsIndex].position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        if (Vector2.Distance(transform.position, Wpoints.waypoints[waypointsIndex].position) < 0.1f)
        {
            if (waypointsIndex < Wpoints.waypoints.Length - 1)
            {
                waypointsIndex++;
            }
            else
            {
                Destroy(gameObject);
            }


        }





    }
}
