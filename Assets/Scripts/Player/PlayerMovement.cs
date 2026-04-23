using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Referances")]
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private GameObject aimDirection;

    [Header("Movement")]
    public bool movementEnabled = true;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 500f;

    [Header("Input")]
    [SerializeField] private KeyCode keyCodeUp = KeyCode.W, keyCodeDown = KeyCode.S, keyCodeLeft = KeyCode.A, keyCodeRight = KeyCode.D;


    private PlayerInput playerInput;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 lookInput;
    private bool isKnockback;
    

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //MoveTargetInput();
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    void MoveTargetInput()
    {
        if (Input.GetKeyUp(keyCodeUp)||Input.GetKeyUp(keyCodeDown))
            moveInput.y = 0;
        
        if (Input.GetKeyUp(keyCodeLeft)||Input.GetKeyUp(keyCodeRight))
            moveInput.x = 0;
        
        if (!movementEnabled) return;
        if (isKnockback) return;

        if (Input.GetKey(keyCodeUp))
            moveInput.y = 1;
        
        if (Input.GetKey(keyCodeDown))
            moveInput.y = -1;
        
        if (Input.GetKey(keyCodeLeft))
            moveInput.x = -1;

        if (Input.GetKey(keyCodeRight))
            moveInput.x = 1;
        
        
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        Debug.Log("Move!");
    }

    public void OnLook(InputValue value)
    {
        lookInput = value.Get<Vector2>();
        Debug.Log("Look!");
    }

    public void OnJump(InputValue value)
    {
        if (value.isPressed)
        {
            Debug.Log("Jump!");
        }
    }

    //Moves the player to the target position
    void MovePlayer()
    {
        if (!movementEnabled) return;
        if (isKnockback) return;
        rb.MovePosition(rb.position + moveInput.normalized * moveSpeed * Time.fixedDeltaTime);

        if (lookInput != Vector2.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, lookInput);
            aimDirection.transform.rotation = Quaternion.RotateTowards(aimDirection.transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        } else if (moveInput != Vector2.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, moveInput);
            aimDirection.transform.rotation = Quaternion.RotateTowards(aimDirection.transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }
    

    void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Hazard")) return;
        if (playerHealth.death) return;
        if (playerHealth.invinsible) return;
        HazardScript hazard = collision.gameObject.GetComponent<HazardScript>();
        if (hazard == null) {
            Debug.LogWarning("Hazard is missing script"); return; }
                
        playerHealth.LoseHealth(hazard.damageAmount, hazard.damageTime);

        if (hazard.dealKnockback == true)
        {
            Vector2 knockbackDir = (transform.position - collision.transform.position).normalized;
            ApplyKnockback(knockbackDir, hazard.knockbackForce, hazard.knockbackDuration, hazard.stunDuration);
        }
        if (hazard.destroyOnTrigger == true)
            Destroy(collision.gameObject);        
    }

    public void ApplyKnockback(Vector2 direction, float force, float duration, float stun) {
        StartCoroutine(KnockbackCoroutine(direction, force, duration, stun));
    }

    private IEnumerator KnockbackCoroutine(Vector2 direction, float force, float duration, float stun) {
        isKnockback = true;
        rb.linearVelocity = Vector2.zero; // Reset velocity for consistency
        rb.AddForce(direction * force, ForceMode2D.Impulse); // Apply instant force
        yield return new WaitForSeconds(duration);
        rb.linearVelocity = Vector2.zero;
        yield return new WaitForSeconds(stun);
        isKnockback = false;
    }



    
}