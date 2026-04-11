using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Referances")]
    [SerializeField] private GameObject targetObject;
    [SerializeField] private PlayerHealth playerHealth;

    [Header("Movement")]
    [SerializeField] private float speed = 5;
    [SerializeField] private float targetDistanceOnMove = 0.5f;

    [Header("Input")]
    [SerializeField] private KeyCode keyCodeUp = KeyCode.W;
    [SerializeField] private KeyCode keyCodeDown = KeyCode.S;
    [SerializeField] private KeyCode keyCodeLeft = KeyCode.A;
    [SerializeField] private KeyCode keyCodeRight = KeyCode.D;

    private Rigidbody2D rb;
    private float targetSetPositionX;
    private float targetSetPositionY;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        MoveTarget();
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    void MoveTarget()
    {
        if (Input.GetKey(keyCodeUp))
        {
            targetSetPositionY = transform.position.y + targetDistanceOnMove;
        }
        if (Input.GetKey(keyCodeDown))
        {
            targetSetPositionY = transform.position.y - targetDistanceOnMove;
        }
        if (Input.GetKey(keyCodeLeft))
        {
            targetSetPositionX = transform.position.x - targetDistanceOnMove;
        }
        if (Input.GetKey(keyCodeRight))
        {
            targetSetPositionX = transform.position.x + targetDistanceOnMove;
        }
    }

    //Detect which side the collition happend to prevent drag
    void OnCollisionStay2D(Collision2D collision)
    {
        Vector2 normal = collision.GetContact(0).normal;
        //Up
        if (normal.y > 0.5f && targetSetPositionY < transform.position.y)
        {
            targetSetPositionY = transform.position.y;
        } 
        //Down
        else if (normal.y < -0.5f && targetSetPositionY > transform.position.y)
        {
            targetSetPositionY = transform.position.y;
        } 
        //Right
        else if (normal.x > 0.5f && targetSetPositionX < transform.position.x)
        {
            targetSetPositionX = transform.position.x;
        } 
        //Left
        else if (normal.x < -0.5f && targetSetPositionX > transform.position.x)
        {
            targetSetPositionX = transform.position.x;
        } 
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Hazard"))
        {
            HazardScript hazard = collision.gameObject.GetComponent<HazardScript>();
            if (hazard != null)
            {
                playerHealth.LoseHealth(hazard.damageAmount);
                if (hazard.destroyOnTrigger == true)
                    Destroy(collision.gameObject);
            }
        } 
    }

    //Moves the player to the target position
    void MovePlayer()
    {
        targetObject.transform.position = new Vector3(
                targetSetPositionX,
                targetSetPositionY,
                0);
        Vector2 targetPosition = targetObject.transform.position;
        float step = speed * Time.deltaTime;

        Vector2 newPosition = Vector2.MoveTowards(rb.position, targetPosition, step);
        rb.MovePosition(newPosition);
    }

    
}