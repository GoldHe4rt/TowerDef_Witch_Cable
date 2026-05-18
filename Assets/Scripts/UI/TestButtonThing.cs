using System.Linq;
using AudioScripts;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class TestButtonThing : MonoBehaviour
    {
        [SerializeField]private Button[] buttons;
        [SerializeField]private PlaySound playSound;

        //Finds all buttons in hierarchy. Called before start.
        private void Awake()
        { 
            buttons = Resources.FindObjectsOfTypeAll<Button>().Where(button => button.gameObject.activeInHierarchy).ToArray();
        }

        //Add listener to buttons on start
        private void Start()
        {
            foreach (var button in buttons)
            {
                button.onClick.AddListener(playSound.PlayButton);
            }
        }
    }
}
