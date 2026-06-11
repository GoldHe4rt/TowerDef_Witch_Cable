using System.Data.Common;
using NavMeshPlus.Extensions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]

public class NavMeshPathfinding : MonoBehaviour
{
    [SerializeField] private Transform targetTransform;

    [Header("Pathfinding")]
    public float AttackDistance;
    public bool isStopped;
    public bool AttackObstacle;
    internal Transform TargetBase;
    private Transform TargetBarricade;

    [Header("Movement")]
    [SerializeField] public bool isActive = true;
    [SerializeField] private Vector2 minMaxSpeed = new Vector2(1f, 15f);
    [SerializeField] private float speed = 5f;
    
    [SerializeField] internal float speedModifier = 1;

    internal float speedMultiplier = 1f;
    private float currentSpeed;
    private NavMeshAgent agent;
    private Animator animator;
    private float distance;
    internal bool stuck = false;



    void Start()
    {
        speed = speed * speedMultiplier;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        agent.destination = TargetBase.position;

        agent.avoidancePriority = Random.Range(10, 65);
        float RadiusValue = Random.Range(0.5f, 0.8f);
        agent.radius = Mathf.Round(RadiusValue * 10) * 0.1f;
    }

    void Update()
    {
        currentSpeed = speed * speedModifier;
        agent.speed = currentSpeed;
        if (stuck)
        {
            transform.position = Vector2.MoveTowards(transform.position, agent.nextPosition, speed * speedModifier * Time.deltaTime);
        }
        else
        {
            if (targetTransform != null)
            {
                transform.position = Vector2.MoveTowards(transform.position, agent.nextPosition, speed * speedModifier * Time.deltaTime);

                if (agent.pathStatus != NavMeshPathStatus.PathComplete)
                {
                    stuck = true;

                }
            }

            FreezeEnemyRotation();
        }



        /*/
                
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
        /*/
    }

    private void FreezeEnemyRotation()
    {
        Vector3 currentRotation = transform.eulerAngles;

        //currentRotation.y = 0f;
        currentRotation.x = 0f;

        transform.eulerAngles = currentRotation;
    }

    internal void SetDifficultyModifier(float difficultyModifier)
    {
        float ClampedDifficultyModifier = difficultyModifier / 100f; // Ensure the difficulty modifier is within range
        float currentMaxSpeed = minMaxSpeed.y * ClampedDifficultyModifier;
        speed = Random.Range(minMaxSpeed.x, currentMaxSpeed);
    }

    internal IEnumerator SpeedRecovery(float duration, float exponent, float startValue, float endValue)
    {
        Debug.Log("Speed recovery initiated.");
        float timeElapsed = 0f;
        speedModifier = startValue;

        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;

            // 1. Calculate normalized time (always 0 to 1)
            float t = timeElapsed / duration;

            // 2. Apply the exponential curve to the progress fraction
            float exponentialT = Mathf.Pow(t, exponent);

            // 3. Interpolate between start and end values
            speedModifier = Mathf.Lerp(startValue, endValue, exponentialT);

            yield return null; // Wait for the next frame
        }

        // Ensure it strictly ends exactly at the destination value
        speedModifier = endValue;

    }
}
