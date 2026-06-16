using System.Security.Cryptography;
using UnityEngine;

public class Freezerotation : MonoBehaviour

{
    [SerializeField] private Transform parent;
    private SpriteRenderer sr;
    private float oldPositionX;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    void Update()
    {

        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, parent.transform.rotation.z * -1f);

        float differance;
        differance = transform.position.x - oldPositionX;

        if (differance < 0.001f)
        {
            sr.flipX = true;

        }
        else if (differance > 0.001f)
        {
            sr.flipX = false;
        }
        Debug.Log(differance);

        oldPositionX = transform.position.x;
    }
}
