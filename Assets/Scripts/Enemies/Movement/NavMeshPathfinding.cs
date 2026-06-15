
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
    [SerializeField] private EnemyRange enemyRange;

    [Header("Pathfinding")]
    public float AttackDistance;
    public bool isStopped;
    public bool AttackObstacle;
    public Transform TargetBase;
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

    private bool canUnstuck = false;
    private float timeBeforeCanUnstuck = 1f;

    void Start()
    {
        speed = speed * speedMultiplier;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        agent.destination = TargetBase.position;

        agent.avoidancePriority = Random.Range(10, 65);
        float RadiusValue = Random.Range(0.5f, 0.8f);
        agent.radius = Mathf.Round(RadiusValue * 10) * 0.1f;


        currentSpeed = speed * speedModifier;
        agent.speed = currentSpeed;
    }

    void Update()
    {
        Debug.Log("stuck:  " + stuck);
        Debug.Log("Partial:  " + (agent.pathStatus == NavMeshPathStatus.PathPartial));
        Debug.Log("invalid:  " + (agent.pathStatus == NavMeshPathStatus.PathInvalid));
        Debug.Log("complete:  " + (agent.pathStatus == NavMeshPathStatus.PathComplete));
        if (agent.pathStatus != NavMeshPathStatus.PathComplete || agent.pathStatus == NavMeshPathStatus.PathInvalid || agent.pathStatus == NavMeshPathStatus.PathPartial)
        {
            if (!stuck)
            {
                Debug.Log("stuck");
                stuck = true;
                //agent.destination = transform.position;
                findNearestBarricade();
                canUnstuck = false;
                //StartCoroutine(waitBeforeCanUnStuck());
            }
            StartCoroutine(canSeeCamp());

        }
        else
        {
            if (stuck && canUnstuck)
            {
                Debug.Log("unstuck");
                stuck = false;
                agent.destination = TargetBase.position;
            }
        }

        if (agent.pathPending)
        {
            canUnstuck = false;
        }

    }
    IEnumerator waitBeforeCanUnStuck()
    {
        yield return new WaitForSeconds(timeBeforeCanUnstuck);
        //canUnstuck = true;
    }
    IEnumerator canSeeCamp()
    {
        yield return new WaitForSeconds(5f);
        agent.destination = TargetBase.position;
        //yield return new WaitForSeconds(0.5f);

        while (agent.pathPending)
            yield return null;

        if (agent.pathStatus != NavMeshPathStatus.PathComplete)
        {
            findNearestBarricade();
            StartCoroutine(canSeeCamp());
        }
        else
        {
            canUnstuck = true;
        }

    }


    void findNearestBarricade()
    {

        Debug.Log("findBC");
        GameObject[] barricades = GameObject.FindGameObjectsWithTag("Barricade");

        GameObject barricadePosition = barricades[0];

        foreach (GameObject barricadeInScene in barricades)
        {
            float b1 = Vector2.Distance(transform.position, barricadeInScene.transform.position);
            float b2 = Vector2.Distance(transform.position, barricadePosition.transform.position);
            if (b1 < b2)
            {
                barricadePosition = barricadeInScene;
            }

        }
        agent.destination = barricadePosition.transform.position;

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
