using Menu;
using UnityEngine;

public class ControlsPopupManager : MonoBehaviour
{
    public GameObject controlsUI;
    [SerializeField] private PauseManager pauseManager;
    private void Start()
    { controlsUI.SetActive(false); }

    public void ShowControls()
    {
        controlsUI.SetActive(true);
        pauseManager.ForcePauseWithoutMenu();
    }

    public void HideControls()
    {
        controlsUI.SetActive(false);
        pauseManager.pauseMenuUI.SetActive(true);
    }
}
