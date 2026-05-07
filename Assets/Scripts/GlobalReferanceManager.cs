using Menu;
using UnityEngine;

public class GlobalReferanceManager : MonoBehaviour
{
    //No values in this script should be changed outside of the inspector, this is just a middle ground

    //One Script to add referances between scripts without having to go all over
    public PauseManager pauseManager;
    public ControlsMenuManager controlsMenuManager;


    //Values to change on a Global space
    public bool buildingEnabled = true;

}
