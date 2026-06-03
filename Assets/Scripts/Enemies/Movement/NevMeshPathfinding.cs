using System.Data.Common;
using NavMeshPlus.Extensions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class NevMeshPathfinding : MonoBehaviour
{
    private Transform TargetBase;
    private Transform TargetBarricade;
    public float AttackDistance;
    public bool isStopped;
    public bool AttackObstacle;
    [SerializeField] public bool isActive = true;
    [SerializeField] private Vector2 minMaxSpeed = new Vector2(1f, 15f);
    [SerializeField] private float speed = 5f;
    [SerializeField] internal float speedModifier = 1;
    [SerializeField] private float freezeDuration = 2f;


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
        distance = Vector2.Distance(agent.transform.position, TargetBase.position);

        FreezeEnemyRotation();
    }
    private void FreezeEnemyRotation()
    {
        Vector3 currentRotation = transform.eulerAngles;

        //currentRotation.y = 0f;
        currentRotation.x = 0f;

        transform.eulerAngles = currentRotation;
    }
}
