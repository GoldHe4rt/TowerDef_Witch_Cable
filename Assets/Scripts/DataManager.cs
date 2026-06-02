using UnityEngine;

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
        if (!giveDatamanagerControll) 
            return;
        if (waveSpawner != null)
            waveSpawner.difficulty = gameDifficulty;
        
    }

    void OnValidate()
    {
        if (!giveDatamanagerControll) 
            return;
        if (waveSpawner != null)
            waveSpawner.difficulty = gameDifficulty;
    }
}
