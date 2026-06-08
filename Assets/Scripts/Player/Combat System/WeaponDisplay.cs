using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class WeaponDisplay : MonoBehaviour
{
    [SerializeField] private PlayerCombatSystem combatSystem;

    [SerializeField] private GameObject[] selectionIndicators;
    private List<TextMeshProUGUI> cooldownTexts = new List<TextMeshProUGUI>();

    void Start()
    {
        for (int i = 0; i < selectionIndicators.Length; i++)
        {
            var cooldownDisplay = selectionIndicators[i].transform.Find("Canvas/Cooldown");
            cooldownTexts.Add(cooldownDisplay.GetComponent<TextMeshProUGUI>());
        }

        UpdateSelectionDisplay(combatSystem.currentWeaponID);
    }

    void Update()
    {
        for (int i = 0; i < cooldownTexts.Count; i++)
        {
            float cooldown = combatSystem.currentCoolDowns[i];
            if (cooldown > 0)
            {
                cooldownTexts[i].text = cooldown.ToString("0.0") + "s";
            } else
            {
                cooldownTexts[i].text = "";
            }
        }
    }

    internal void UpdateSelectionDisplay(int id)
    {
        //Change size of selection indicator based on distance from id
        for (int i = 0; i < selectionIndicators.Length; i++)
        {
            var disabledDisplay = selectionIndicators[i].transform.Find("Front");
            
            disabledDisplay.gameObject.SetActive(i != id);
        
            GameObject indicator = selectionIndicators[i];
            RectTransform rectTransform = indicator.GetComponent<RectTransform>();
            float scale = 1f;
            // Set scale based on distance from id
            if (i == id)
            {
                rectTransform.sizeDelta = new Vector2(6f, 9f);
                scale = 12f;
            } else
            {
                
                rectTransform.sizeDelta = new Vector2(6f, 10f); 
                scale = 10f;
            }
            indicator.transform.localScale = new Vector3(scale, scale, scale);
        }
    }
}
