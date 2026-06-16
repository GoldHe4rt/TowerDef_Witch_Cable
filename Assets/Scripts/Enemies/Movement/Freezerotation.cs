using UnityEngine;

public class Freezerotation : MonoBehaviour

{
    private Rigidbody2D rb;

    void Start()
    {
        // Get the Rigidbody2D component attached to the sprite
        rb = GetComponent<Rigidbody2D>();

        // Freeze the Z-axis rotation from physics calculations
        rb.freezeRotation = true;
    }
}
