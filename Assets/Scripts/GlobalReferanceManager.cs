using Menu;
using UnityEngine;

public class GlobalReferanceManager : MonoBehaviour
{
    //No values in this script should be changed outside of the inspector, this is just a middle ground

    //One Script to add referances between scripts without having to go all over
    [Header("Outside References")]
    public PauseManager pauseManager;
    public ControlsMenuManager controlsMenuManager;
    public CampHealth campHealth;


    //Values to change on a Global space
    [Header("Global Settings")]
    public bool buildingEnabled = true;
    public Currency currency = Currency.SeperateBanks;
    public float campStartCurrency = 2500f;
    public float playerStartCurrency = 1000f;

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
