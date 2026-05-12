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
            StartCoroutine(SetFirstSelectedDelay());
        }

        private IEnumerator SetFirstSelectedDelay()
        {
            //Wait one frame to make sure the UI is ready
            yield return null;
            
            //Clear previous selection
            myEventSystem.SetSelectedGameObject(null);
            
            //Set a new selected game object
            myEventSystem.SetSelectedGameObject(selectUI);
            
            //Force highlight for controller!!
            var selectable = selectUI.GetComponent<Selectable>();
            if (selectUI != null)
            {
                selectable.OnSelect(null);
            }
        }
        
        //Change selection at runtime.
        /*public void ChangeSelection(GameObject newSelectUI)
        {
            if (newSelectUI == null) return;
            
            //Clear previous selection
            myEventSystem.SetSelectedGameObject(null);
            
            //Set the new selected UI
            myEventSystem.SetSelectedGameObject(newSelectUI);

            var selectable = newSelectUI.GetComponent<Selectable>();
            if (selectable != null)
            {
                selectable.OnSelect(null);
            }
        }*/
    }
}
