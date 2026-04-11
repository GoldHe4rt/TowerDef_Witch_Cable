using UnityEngine;
using TMPro;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    [Header("Referances")]
    [SerializeField] private TextMeshProUGUI healthDisplay;
    [SerializeField] private GameObject canTakeDamageDisplay;

    [Header("Health")]
    [SerializeField] private int healthPoints = 5;
    [SerializeField] private float damageFrames = 1f;
    

    private bool Death = false;
    private bool invinsible = false;

    void Start()
    {
        healthDisplay.text = healthPoints.ToString("0");
        canTakeDamageDisplay.SetActive(false);
    }

    void Update()
    {
        if (healthPoints <= 0 && Death == false)
        {
            Death = true;
            Debug.Log("You are out of Health");
        }
    }
    
    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Trigger");
        if (other.gameObject.CompareTag("Hazard"))
        {
            Debug.Log("Hit!");
            Destroy(other.gameObject);
            LoseHealth(1);
        } 
    }
    
    public void LoseHealth(int amount)
    {
        if (invinsible == false && healthPoints > 0)
        {
            invinsible = true;
            canTakeDamageDisplay.SetActive(true);
            healthPoints = healthPoints - amount;
            healthDisplay.text = healthPoints.ToString("0");
            Debug.Log("Took " + amount + " Damage!");

            StartCoroutine(TakeDamage());
        }
        
    }

    IEnumerator TakeDamage()
    {
        yield return new WaitForSeconds(damageFrames);
        canTakeDamageDisplay.SetActive(false);
        invinsible = false;
        
    }
}
