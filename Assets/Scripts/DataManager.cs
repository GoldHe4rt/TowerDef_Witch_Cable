using UnityEngine;

public class DataManager : MonoBehaviour
{
    [Header("IMPORTANT")]
    public bool giveDatamanagerControll = true;
    [Header("Difficulty")]
    [SerializeField] private WaveSpawner waveSpawner;
    [SerializeField] private GameDifficulty gameDifficulty;
    

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
        waveSpawner.difficulty = gameDifficulty;
    }

}
