using UnityEngine;

public class Freezerotation : MonoBehaviour

{
    [SerializeField] private Transform parent;
    private SpriteRenderer sr;
    private Transform oldPosition;
    private Transform currentPosition;

    void Start()
    {
        rb = parent.GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        sr.transform.position =
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, parent.transform.rotation.z * -1f);
        if (rb.linearVelocity.x < 0f)
        {
            transform.localScale = new Vector2(-1, 1);
        }
        else
        {
            transform.localScale = new Vector2(1, 1);
        }
        Debug.Log(rb.linearVelocity.x);
    }
}
