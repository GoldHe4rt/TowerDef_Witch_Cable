using UnityEngine;
using System.Collections;

public class DestroyAfterTime : MonoBehaviour
{
    [SerializeField] private float Time = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(DestroyAfterTimer());
    }

    private IEnumerator DestroyAfterTimer()
    {
        yield return new WaitForSeconds(Time);
        Destroy(gameObject);
    }
}
