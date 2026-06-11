using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class BackToMainMenu : MonoBehaviour
{
    [SerializeField] private InputActionReference backAction;
    
    private void Update()
    {
        if (backAction.ToInputAction().triggered && SceneManager.GetActiveScene().name == "LevelSelect")
            SceneManager.LoadScene("PlayerSelect");
        else if (backAction.ToInputAction().triggered) SceneManager.LoadScene(sceneBuildIndex: 0);
    }
    
}
