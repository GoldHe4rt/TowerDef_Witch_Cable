using Menu;
using UnityEngine;

public class GlobalInput : MonoBehaviour
{

    [SerializeField] private PauseManager pauseManager;
    [SerializeField] private ControlsMenuManager controlsMenuManager;

    public void Pause()
    {
        if (!pauseManager.isPaused)
            pauseManager.Pause();
        else pauseManager.Resume();
    }

    public void ShowControlls()
    {
        Debug.Log("aaaaaaaa");
        if (!controlsMenuManager.isActive)
            controlsMenuManager.ShowControls();
        else controlsMenuManager.HideControls();
    }
}
