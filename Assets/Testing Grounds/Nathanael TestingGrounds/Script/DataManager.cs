using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance; // Static reference to the singleton instance
    

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject); // Destroy new instances if one already exists
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Keep this object alive
    }
}