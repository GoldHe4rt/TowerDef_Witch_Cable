using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.UI;

namespace Menu
{
    public class CurrentSelectedUI : MonoBehaviour
    {
        private EventSystem myEventSystem;
        [SerializeField] private GameObject selectUI;
        
        private void OnEnable()
        {
            myEventSystem = EventSystem.current;
            StartCoroutine(SetSelectedDelay());
        }

        private IEnumerator SetSelectedDelay()
        {
            //Wait one frame to make sure the UI is ready
            yield return null;
            
            //Clear previous selection
            myEventSystem.SetSelectedGameObject(null);
            
            //Set a new selected game object
            myEventSystem.SetSelectedGameObject(selectUI);
            
            //Force highlight for controller!!
            Selectable selectable = selectUI.GetComponent<Selectable>();
            if (selectUI != null)
            {
                selectable.OnSelect(null);
            }
        }
        
        //Change selection at runtime.
        /*public void ChangeSelection()
        {
            if (selectUI == null) return;
            
            //Clear previous selection
            myEventSystem.SetSelectedGameObject(null);
            
            //Set the new selected UI
            myEventSystem.SetSelectedGameObject(selectUI);

            Selectable selectable = selectUI.GetComponent<Selectable>();
            if (selectable != null)
            { selectable.OnSelect(null); }
        }*/
    }
}
