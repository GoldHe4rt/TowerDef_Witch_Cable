using Menu;
using UnityEngine;

public class GlobalReferanceManager : MonoBehaviour
{
    //No values in this script should be changed outside of the inspector, this is just a middle ground

    //One Script to add referances between scripts without having to go all over
    [Header("Global References")]
    public PauseManager pauseManager;
    public ControlsMenuManager controlsMenuManager;


    //Values to change on a Global space
    [Header("Global Settings")]
    public bool buildingEnabled = true;
    public bool currencyEnabled = true;
    public bool splitCurrency = true;
    

}

public enum Currencytype
{
    Gold,
    Gems
}
