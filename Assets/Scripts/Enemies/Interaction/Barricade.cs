using UnityEngine;
using UnityEngine.AI;

public class Barecade : MonoBehaviour

{
    private NavMeshAgent agent;
    public Transform waypoint;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        agent.SetDestination(waypoint.position);
    }
}
