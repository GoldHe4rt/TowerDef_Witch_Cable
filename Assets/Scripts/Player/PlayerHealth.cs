using UnityEngine;
using TMPro;
using System.Collections;
using Unity.VisualScripting;

public class PlayerHealth : MonoBehaviour
{
    [Header("Referances")]
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private TextMeshProUGUI healthDisplay;
    [SerializeField] private GameObject canTakeDamageDisplay;
    

    [Header("Health")]
    [SerializeField] private int healthPoints = 5;
    [SerializeField] private bool knockbackEnabled = true;
    [SerializeField] private bool iFramesEnabled = true;
    
    [HideInInspector] public bool death = false;
    [HideInInspector] public bool invinsible = false;


    void Start()
    {
        healthDisplay.text = healthPoints.ToString("0");
        canTakeDamageDisplay.SetActive(false);
    }

    void Update()
    {
        if (healthPoints <= 0 && death == false)
        {
            death = true;
            Debug.Log("You are out of Health");
        }
    }
    
    
    public void LoseHealth(int damageAmount, float damageFrames)
    {
        healthPoints = healthPoints - damageAmount;
        healthDisplay.text = healthPoints.ToString("0");
        Debug.Log("Took " + damageAmount + " Damage!");

        if (iFramesEnabled == true)
        {
            invinsible = true;
            canTakeDamageDisplay.SetActive(true);
            StartCoroutine(IFrames(damageFrames));
        }
            
    }

    IEnumerator IFrames(float damageFrames)
    {
        yield return new WaitForSeconds(damageFrames);
        canTakeDamageDisplay.SetActive(false);
        invinsible = false;
    }


    public IEnumerator Knockback(Collider2D other, float amount)
    {
        if (!knockbackEnabled)
            yield break;
        playerMovement.movementEnabled = false;

        float targetX = playerMovement.transform.position.x + (playerMovement.transform.position.x - other.transform.position.x)*amount;
        float targetY = playerMovement.transform.position.y + (playerMovement.transform.position.y - other.transform.position.y)*amount;
        playerMovement.targetObject.transform.position = new Vector3(
                targetX,
                targetY,
                0);
        playerMovement.modifier = 1 + amount * 0.2f;
        yield return new WaitForSeconds(0.5f);
        playerMovement.movementEnabled = true;
    }
}
