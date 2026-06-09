using UnityEngine;
using UnityEngine.Events;

public class HealthUI : MonoBehaviour
{
    public GameObject[] lifeUI;
    private UnityEvent loseLifeEvent;
    private PlayerHealth playerHealth;
    public int currentHealth = 5;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        loseLifeEvent ??= new UnityEvent();

        foreach (var life in lifeUI) life.SetActive(true);

        loseLifeEvent.AddListener(LoseLife);
        
    }

    private void UpdateHealth(int healthAmount)
    {
        currentHealth = healthAmount;
        loseLifeEvent.Invoke();
    }
    public void LoseLife()
    {
        switch (currentHealth)
        {
            case 5:
            {
                foreach (var life in lifeUI) life.SetActive(true);
                break;
            }
            case 4:
                lifeUI[4].SetActive(false);
                break;
            case 3:
                lifeUI[4].SetActive(false);
                lifeUI[3].SetActive(false);
                break;
            case 2:
                lifeUI[4].SetActive(false);
                lifeUI[3].SetActive(false);
                lifeUI[2].SetActive(false);
                break;
            case 1:
                lifeUI[4].SetActive(false);
                lifeUI[3].SetActive(false);
                lifeUI[2].SetActive(false);
                lifeUI[1].SetActive(false);
                break;
            case 0:
                lifeUI[4].SetActive(false);
                lifeUI[3].SetActive(false);
                lifeUI[2].SetActive(false);
                lifeUI[1].SetActive(false);
                lifeUI[0].SetActive(false);
                break;
        }
    }
}
