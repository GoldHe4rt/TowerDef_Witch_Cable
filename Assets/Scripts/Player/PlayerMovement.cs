using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Referances")]
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private GameObject aimDirection;
    

    [Header("Movement")]
    public bool movementEnabled = true;
    internal bool knockbackRunning = false;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 500f;
    internal float movementSpeedModifier = 1f;


    //private PlayerInput playerInput;
    private Rigidbody2D rb;
    [HideInInspector] public Vector2 moveInput;
    [HideInInspector] public  Vector2 lookInput;
    


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    

    void FixedUpdate()
    {
        MovePlayer();
    }

    //Moves the player to the target position
    void MovePlayer()
    {
        if (!movementEnabled || knockbackRunning) return;
        rb.MovePosition(rb.position + moveInput.normalized * moveSpeed * movementSpeedModifier * Time.fixedDeltaTime);

        if (lookInput != Vector2.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, lookInput);
            aimDirection.transform.rotation = Quaternion.RotateTowards(aimDirection.transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
        else if (moveInput != Vector2.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, moveInput);
            aimDirection.transform.rotation = Quaternion.RotateTowards(aimDirection.transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }

    //Knockback
    public void ApplyKnockback(Vector2 direction, float force, float duration, float stun, float damageTime)
    {
        StartCoroutine(KnockbackCoroutine(direction, force, duration, stun, damageTime));
    }

    private IEnumerator KnockbackCoroutine(Vector2 direction, float force, float knockbackDuration, float stunTime, float damageTime)
    {
        knockbackRunning = true;
        rb.linearVelocity = Vector2.zero; // Reset velocity for consistency
        rb.AddForce(direction * force, ForceMode2D.Impulse); // Apply instant force
        yield return new WaitForSeconds(knockbackDuration);
        rb.linearVelocity = Vector2.zero;

        // Stun after knockback
        yield return new WaitForSeconds(stunTime);
        knockbackRunning = false;
        
        //Slow Start After Hit
        StartCoroutine(SpeedRecovery(0.5f, 1f, damageTime - knockbackDuration - stunTime, 3f));
    }



    IEnumerator SpeedRecovery(float startValue, float endValue, float duration, float exponent)
    {
        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;
            
            // 1. Calculate normalized time (always 0 to 1)
            float t = timeElapsed / duration;

            // 2. Apply the exponential curve to the progress fraction
            float exponentialT = Mathf.Pow(t, exponent);

            // 3. Interpolate between start and end values
            movementSpeedModifier = Mathf.Lerp(startValue, endValue, exponentialT);

            Debug.Log($"Current Value: {movementSpeedModifier}");
            yield return null; // Wait for the next frame
        }

        // Ensure it strictly ends exactly at the destination value
        movementSpeedModifier = endValue; 
    }

}