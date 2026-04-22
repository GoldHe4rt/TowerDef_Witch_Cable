using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxHealth(int health)
    {
        //Makes the slider start at the max amount of health
        slider.maxValue = health;
        slider.value = health;
    }
    
    public void SetHealth(int health)
    {
        slider.value = health;
    }
   
}
