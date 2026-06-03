using System.Data.Common;
using NavMeshPlus.Extensions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]

public class NevMeshPathfinding : MonoBehaviour
{
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
    [SerializeField] private float freezeDuration = 2f;
    [SerializeField] private bool isFrozen = false;


    private NavMeshAgent agent;
    private Animator animator;
    private float distance;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!isActive) return;
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
