using System.Collections;
using UnityEngine;
using TMPro;

namespace Menu
{
    public class TutorialManager : MonoBehaviour
    {
        //Need something for text stuff which shows after players have pressed a certain button and such.
        public GameObject introObject;
        public GameObject outroObject;
        public GameObject tutorialObject;
        public GameObject levelselectButton;

        [Header("Text settings")]
        [TextArea] [SerializeField] private string[] tutorial;
        [SerializeField] public int currentText;
        [Header("UI elements")][SerializeField]
        private TextMeshProUGUI tutorialText;
        
        
        private void Start()
        {
            HideThings();
            StartCoroutine(Intro());
        }

        private void HideThings()
        {
            introObject.SetActive(false);
            outroObject.SetActive(false);
            tutorialObject.SetActive(false);
            levelselectButton.SetActive(false);
        }
         private IEnumerator Intro()
         {
             introObject.SetActive(true);
             yield return new WaitForSeconds(5);
             introObject.SetActive(false);
             StartCoroutine(Tutorial());
         }
         
         //Needs some testing to make sure everything works the right way.
        private IEnumerator Tutorial()
        {
            tutorialObject.SetActive(true);
            
            tutorialText.SetText(tutorial[currentText]);
            
            tutorialObject.SetActive(false);
            
            yield return new WaitForSeconds(1);
            StartCoroutine(Outro());
        }

        private IEnumerator Outro()
        {
            if (currentText < 6) yield break;
            outroObject.SetActive(true);
            
            yield return new WaitForSeconds(2);
            outroObject.SetActive(false);
            levelselectButton.SetActive(true);
        }
    }
}
