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
    private bool knockbackRunning = false;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 500f;


    //private PlayerInput playerInput;
    private Rigidbody2D rb;
    [HideInInspector] public Vector2 moveInput;
    [HideInInspector] public  Vector2 lookInput;
    


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
/*/
    void Update()
    {
        MoveTargetInput();
    }

    [Header("Input")]
    [SerializeField] private KeyCode keyCodeUp = KeyCode.W, keyCodeDown = KeyCode.S, keyCodeLeft = KeyCode.A, keyCodeRight = KeyCode.D;

    void MoveTargetInput()
    {
        if (Input.GetKeyUp(keyCodeUp) || Input.GetKeyUp(keyCodeDown))
            moveInput.y = 0;

        if (Input.GetKeyUp(keyCodeLeft) || Input.GetKeyUp(keyCodeRight))
            moveInput.x = 0;

        if (!movementEnabled) return;

        if (Input.GetKey(keyCodeUp))
            moveInput.y = 1;

        if (Input.GetKey(keyCodeDown))
            moveInput.y = -1;

        if (Input.GetKey(keyCodeLeft))
            moveInput.x = -1;

        if (Input.GetKey(keyCodeRight))
            moveInput.x = 1;
    }
/*/

    void FixedUpdate()
    {
        MovePlayer();
    }

    //Moves the player to the target position
    void MovePlayer()
    {
        if (!movementEnabled || knockbackRunning) return;
        rb.MovePosition(rb.position + moveInput.normalized * moveSpeed * Time.fixedDeltaTime);

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
    public void ApplyKnockback(Vector2 direction, float force, float duration, float stun)
    {
        StartCoroutine(KnockbackCoroutine(direction, force, duration, stun));
    }

    private IEnumerator KnockbackCoroutine(Vector2 direction, float force, float duration, float stunTime)
    {
        knockbackRunning = true;
        rb.linearVelocity = Vector2.zero; // Reset velocity for consistency
        rb.AddForce(direction * force, ForceMode2D.Impulse); // Apply instant force
        yield return new WaitForSeconds(duration);
        rb.linearVelocity = Vector2.zero;

        // Stun after knockback
        yield return new WaitForSeconds(stunTime);
        knockbackRunning = false;
    }

}