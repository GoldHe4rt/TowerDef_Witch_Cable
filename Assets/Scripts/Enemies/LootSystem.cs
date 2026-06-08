using UnityEngine;

public class LootSystem : MonoBehaviour
{
    [System.Serializable]
    public struct LootItem
    {
        public string itemName;
        [Range(0, 100)] public float dropChance; 
    }

    public LootItem[] possibleLoot;

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
                return;
            }
        }
    }
}
