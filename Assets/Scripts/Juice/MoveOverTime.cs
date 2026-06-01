using UnityEngine;


[System.Serializable]
public class Movement
{
    [SerializeField] public bool moveEnabled = true;
    [SerializeField] public bool LocalMovement = false;
    [SerializeField] public float moveSpeed = 1f;
    [SerializeField] public float duration = 1f;
    internal float currentDuration = 1f;
    [SerializeField] public Vector2 moveDirection2D = Vector2.zero;
    [SerializeField] public Vector3 moveDirection3D = Vector3.zero;
}

[System.Serializable]
public class Rotation
{
    [SerializeField] public bool rotateEnabled = true;
    [SerializeField] public bool randomizeDirection = false;
    [SerializeField] public float rotationSpeed = 1f;
    [SerializeField] public float duration = 1f;
    internal float currentDuration = 1f;
    [SerializeField] public float rotationDirection2D = 0f;
    [SerializeField] public Vector3 rotationDirection3D = Vector3.zero;
}

[System.Serializable]
public class Scale
{
    [SerializeField] public bool scaleEnabled = true;
    [SerializeField] public float scaleSpeed = 1f;
    [SerializeField] public float duration = 5f;
    internal float currentDuration = 1f;
    [SerializeField] public Vector2 scaleDirection2D = Vector2.zero;
    [SerializeField] public Vector3 scaleDirection3D = Vector3.zero;
}

public class MoveOverTime : MonoBehaviour
{

    [Header("Toggles")]
    [SerializeField] private bool isEnabled = true;
    [SerializeField] private bool is2D = true;

    [Header("Movement")]
    [SerializeField] private bool isMovementEnabled = true;
    [SerializeField] private bool loopMovement = false;
    [SerializeField] private Movement[] movement;
    private int activeMovementID = 0;

    [Header("Rotation")]
    [SerializeField] private bool isRotationEnabled = true;
    [SerializeField] private bool loopRotation = false;
    [SerializeField] private Rotation[] rotation;
    private int activeRotationID = 0;

    [Header("Scale")]
    [SerializeField] private bool isScaleEnabled = true;
    [SerializeField] private bool loopScale = false;
    [SerializeField] private Scale[] scale;
    private int activeScaleID = 0;

    void Start()
    {
        for (int i = 0; i < movement.Length; i++)
        {
            movement[i].currentDuration = movement[i].duration;
        }

        for (int i = 0; i < rotation.Length; i++)
        {
            rotation[i].currentDuration = rotation[i].duration;
            if (rotation[activeRotationID].randomizeDirection)
            {
                bool randomBool = Random.value > 0.5f;
                rotation[i].rotationDirection2D = randomBool ? 
                    rotation[i].rotationDirection2D * 1 : 
                    rotation[i].rotationDirection2D * -1;
                rotation[i].rotationDirection3D = randomBool ? 
                    rotation[i].rotationDirection3D * 1 : 
                    rotation[i].rotationDirection3D * -1;
            }
        }

        for (int i = 0; i < scale.Length; i++)
        {
            scale[i].currentDuration = scale[i].duration;
        }
    }

    void Update()
    {
        if (!isEnabled)
            return;
        if (movement.Length > activeMovementID && isMovementEnabled)
        {
            movement[activeMovementID].currentDuration -= Time.deltaTime * movement[activeMovementID].moveSpeed;
            if (movement[activeMovementID].currentDuration <= 0)
            {
                movement[activeMovementID].currentDuration = movement[activeMovementID].duration;
                activeMovementID ++;
                if (activeMovementID >= movement.Length)
                {
                    isMovementEnabled = false;
                    activeMovementID = 0;
                }
            }
            if (movement[activeMovementID].moveEnabled)
                Move();
            
        } else if (loopMovement)
        {
            isMovementEnabled = true;
            activeMovementID = 0;
        }

        if (rotation.Length > activeRotationID && isRotationEnabled)
        {
            rotation[activeRotationID].currentDuration -= Time.deltaTime * rotation[activeRotationID].rotationSpeed;
            if (rotation[activeRotationID].currentDuration <= 0)
            {
                rotation[activeRotationID].currentDuration = rotation[activeRotationID].duration;
                activeRotationID ++;
                if (activeRotationID >= rotation.Length)
                {
                    isRotationEnabled = false;
                    activeRotationID = 0;
                }
            }
            if (rotation[activeRotationID].rotateEnabled)
                Rotate();
        } else if (loopRotation)
        {
            isRotationEnabled = true;
            activeRotationID = 0;
        }

        if (scale.Length > activeScaleID && isScaleEnabled)
        {
            scale[activeScaleID].currentDuration -= Time.deltaTime * scale[activeScaleID].scaleSpeed;
            if (scale[activeScaleID].currentDuration <= 0)
            {
                scale[activeScaleID].currentDuration = scale[activeScaleID].duration;
                activeScaleID ++;
                if (activeScaleID >= scale.Length)
                {
                    isScaleEnabled = false;
                    activeScaleID = 0;
                }
            }
            if (scale[activeScaleID].scaleEnabled)
                Scale();
        } else if (loopScale)
        {
            isScaleEnabled = true;
            activeScaleID = 0;
        }
    }

    void Move()
    {
        float currentMoveSpeed = movement[activeMovementID].moveSpeed;
        Vector2 currentMoveDirection2D = movement[activeMovementID].moveDirection2D;
        Vector3 currentMoveDirection3D = movement[activeMovementID].moveDirection3D;
        
        if (is2D)
        {
            Vector3 direction = movement[activeMovementID].LocalMovement
                ? transform.TransformDirection(currentMoveDirection2D)
                : (Vector3)currentMoveDirection2D;
            transform.position += 
                direction * 
                currentMoveSpeed * 
                Time.deltaTime;
        }
        else
        {
            Vector3 direction = movement[activeMovementID].LocalMovement
                ? transform.TransformDirection(currentMoveDirection3D)
                : (Vector3)currentMoveDirection3D;
            transform.position += 
                direction * 
                currentMoveSpeed * 
                Time.deltaTime;
        }
    }

    void Rotate()
    {
        float currentRotationSpeed = rotation[activeRotationID].rotationSpeed;
        float currentRotationDirection2D = rotation[activeRotationID].rotationDirection2D;
        Vector3 currentRotationDirection3D = rotation[activeRotationID].rotationDirection3D;
        
        
        if (is2D)
        {
            transform.Rotate(0, 0, 
                currentRotationDirection2D * 
                currentRotationSpeed * 
                Time.deltaTime);
        }
        else
        {
            transform.Rotate(
                currentRotationDirection3D * 
                currentRotationSpeed * 
                Time.deltaTime);
        }
    }

    void Scale()
    {
        float currentScaleSpeed = scale[activeScaleID].scaleSpeed;
        Vector2 currentScaleDirection2D = scale[activeScaleID].scaleDirection2D;
        Vector3 currentScaleDirection3D = scale[activeScaleID].scaleDirection3D;

        if (is2D)
        {
            transform.localScale += 
                (Vector3)currentScaleDirection2D * 
                currentScaleSpeed * 
                Time.deltaTime;
        }
        else
        {
            transform.localScale += 
                currentScaleDirection3D * 
                currentScaleSpeed * 
                Time.deltaTime;
        }
    }
}
