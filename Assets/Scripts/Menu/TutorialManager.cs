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

        [Header("Text settings")]
        [TextArea] [SerializeField] private string[] tutorial;
        [SerializeField] public int currentText;
        //[SerializeField] private float textSpeed = 1f;
        //private bool _isScrolling = true;

        
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
        }
         private IEnumerator Intro()
         {
             introObject.SetActive(true);
             yield return new WaitForSeconds(5);
             introObject.SetActive(false);
             StartCoroutine(Tutorial());
         }
         
        //Might need to be an IEnumerator instead.
        //Needs something for the player going through the controls for some of the text in the array.
        private IEnumerator Tutorial()
        {
            tutorialObject.SetActive(true);
            //Start on 0 in the array
            tutorialText.SetText(tutorial[currentText]);
            
            StartCoroutine(Intro());
            tutorialObject.SetActive(false);
            
            yield return new WaitForSeconds(1);
            StartCoroutine(Outro());
        }

        private IEnumerator Outro()
        {
            if (currentText < 6) yield break;
            //The last text bits.
            outroObject.SetActive(true);
            yield return new WaitForSeconds(2);
            //Load level 1
        }
    }
}
