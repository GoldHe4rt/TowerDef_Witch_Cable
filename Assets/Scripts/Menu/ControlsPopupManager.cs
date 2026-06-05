using UnityEngine;

public class ControlsPopupManager : MonoBehaviour
{
    public GameObject controlsUI;
    private void Start()
    { HideControls(); }

    public void ShowControls()
    { controlsUI.SetActive(true); }
    public void HideControls()
    { controlsUI.SetActive(false); }
}
