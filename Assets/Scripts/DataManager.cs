using UnityEngine;
using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour
{
    [Header("IMPORTANT")]
    [SerializeField] public bool giveDatamanagerControll = true;
    [Header("Difficulty")]
    [SerializeField] private WaveSpawner waveSpawner;
    [SerializeField] internal GameDifficulty gameDifficulty;

    void Awake()
    {
        DontDestroyOnLoad(this);
        
    }
    private void OnEnable()
    {
        // Subscribe your custom method to the sceneLoaded delegate
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // Always unsubscribe to prevent memory leaks or errors
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // This method runs automatically right after a new level/scene loads
    private void OnSceneLoaded(Scene Scene, LoadSceneMode mode)
    {
        Debug.Log($"Level loaded: {Scene.name}");
        
        Debug.Log("aaaa");
        if (!giveDatamanagerControll) 
            return;
        if (waveSpawner == null)
        {
            Debug.Log("bbbb");
            waveSpawner = Object.FindFirstObjectByType<WaveSpawner>();
        }
            
        if (waveSpawner != null)
        {
            Debug.Log("cccc");
            waveSpawner.difficulty = gameDifficulty;
        }
        
        // Execute your custom logic here (e.g., spawn player, update UI)
        RunLevelChangeLogic(Scene.buildIndex);
    }

    private void RunLevelChangeLogic(int levelIndex)
    {
        // Add your custom logic here
    }

    void OnValidate()
    {
        if (!giveDatamanagerControll) 
            return;
        if (waveSpawner != null)
            waveSpawner.difficulty = gameDifficulty;
    }
}
