using UnityEngine;

public class LootSystem : MonoBehaviour
{
    [System.Serializable]
    public struct LootItem
    {
        public string itemName;
        public GameObject dropItem;
        [Range(0, 100)] public float dropChance; 
    }

    public LootItem[] possibleLoot;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            RollLoot();
        }
    }

    public void RollLoot()
    {
        // 1. Calculate the total weight of all items
        float totalChance = 0f;
        foreach (LootItem item in possibleLoot)
        {
            totalChance += item.dropChance;
        }

        // 2. Pick a random number within the total
        float randomRoll = Random.Range(0f, totalChance);

        // 3. Find which item the roll landed on
        float cumulativeChance = 0f;
        foreach (LootItem item in possibleLoot)
        {
            cumulativeChance += item.dropChance;
            if (randomRoll <= cumulativeChance)
            {
                Debug.Log($"You rolled: {item.itemName}");
                if (item.dropItem != null)
                    Instantiate(item.dropItem, transform.position, Quaternion.identity);
                return;
            }
        }
    }
}
