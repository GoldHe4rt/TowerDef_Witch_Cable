using System.Data.Common;
using NavMeshPlus.Extensions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class NevMeshPathfinding : MonoBehaviour
{
    public Transform Target;
    public float AttackDistance;
    public PathFinding pathFindinfScript;
    public bool isStopped;
    public bool AttackObstacle;

    private NavMeshAgent agent;
    private Animator animator;
    private float distance;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        distance = Vector2.Distance(agent.transform.position, Target.position);
        if (distance < AttackDistance)
        {
            agent.isStopped = true;
            animator.SetBool("Attack", true);
        }
        else
        {
            agent.isStopped = false;
            animator.SetBool("Attack", false);
        }

        if (!agent.hasPath)
        {
            agent.isStopped = false;
            pathFindinfScript.ChangeWaypoint();
        }
    }

    void AnimatorMove()
    {
        if (animator.GetBool("Attack") == false)
        {
            agent.speed = (animator.deltaPosition / Time.deltaTime).magnitude;
        }
    }
}
