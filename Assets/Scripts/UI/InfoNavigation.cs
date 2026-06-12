using UnityEngine;
using UnityEngine.UI;

public class InfoNavigation : MonoBehaviour
{
    [SerializeField] private Button nextButton;
    [SerializeField] private GameObject[] infoUI;
    private GameObject uitoShow;
    public int clickAmount;

    private void Start()
    {
        clickAmount = 0;
        infoUI[0].SetActive(true);
        for (int i = 1; i < 3; i++) infoUI[i].SetActive(false);
    }

    private void ShowandHideUI(int uiIndex)
    {
        switch (uiIndex)
        {
            case 0:
                uitoShow = infoUI[0];
                uitoShow.SetActive(true);
                break;
            case 1:
                uitoShow = infoUI[1];
                uitoShow.SetActive(true);
                infoUI[2].SetActive(false);
                infoUI[3].SetActive(false);
                infoUI[0].SetActive(false);
                break;
            case 2:
                uitoShow = infoUI[2];
                uitoShow.SetActive(true);
                infoUI[3].SetActive(false);
                infoUI[0].SetActive(false);
                infoUI[1].SetActive(false);
                break;
            case 3:
                uitoShow = infoUI[3];
                uitoShow.SetActive(true);
                infoUI[0].SetActive(false);
                infoUI[1].SetActive(false);
                infoUI[2].SetActive(false);
                break;
        }
    }

    
    
    public void ClickNext()
    {
        switch (clickAmount++)
        {
            case 0: Debug.Log("Case 0");
                ShowandHideUI(1);
                break;
            case 1: Debug.Log("Case 1");
                ShowandHideUI(2);
                break;
            case 2: Debug.Log("Case 2");
                ShowandHideUI(3);
                break;
            case 3: Debug.Log("Case 3");
                break;
        }
    }
}
