using Menu;
using UnityEngine;
using TMPro;
using AudioScripts;

public class GlobalReferanceManager : MonoBehaviour
{
    //No values in this script should be changed outside of the inspector, this is just a middle ground

    //One Script to add referances between scripts without having to go all over
    [Header("Outside References")]
    public PauseManager pauseManager;
    public AudioEventManager soundManager;
    public PlayerStats playerStats;
    public CampHealth campHealth;
    public GameOverController gameOverController;
    //public WaveSpawner waveSpawner;

    void Start()
    {
        if (pauseManager == null)
            Debug.LogWarning("PauseManager reference is not assigned in GlobalReferanceManager.");
        if (soundManager == null)
            Debug.LogWarning("AudioEventManager reference is not assigned in GlobalReferanceManager.");
        if (playerStats == null)
            Debug.LogWarning("PlayerStats reference is not assigned in GlobalReferanceManager.");
        if (campHealth == null)
            Debug.LogWarning("CampHealth reference is not assigned in GlobalReferanceManager.");
        if (gameOverController == null)
            Debug.LogWarning("GameOverController reference is not assigned in GlobalReferanceManager.");
        //if (waveSpawner == null)
        //    Debug.LogWarning("WaveSpawner reference is not assigned in GlobalReferanceManager.");
    }

    [Header("Inside References")]
    [SerializeField] public TextMeshProUGUI waveDisplay;

    //Values to change on a Global space
    [Header("Global Settings")]
    public bool buildingEnabled = true;
    public Currency currency = Currency.SeperateBanks;
    public float startCurrency = 2500f;
}

public enum Currency
{
    None,
    SharedBank,
    SeperateBanks,
    SplitEvenly
}

public enum Currencytype
{
    Gold,
    Gems
}
