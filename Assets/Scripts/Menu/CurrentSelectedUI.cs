using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

namespace Menu
{
    public class CurrentSelectedUI : MonoBehaviour
    {
        [SerializeField]private EventSystem myEventSystem;
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
            
            myEventSystem.SetSelectedGameObject(null);
            myEventSystem.SetSelectedGameObject(selectUI);
            
            //Force highlight for controller!!
            Selectable selectable = selectUI.GetComponent<Selectable>();
            
            if (selectUI != null) selectable.OnSelect(null);
        }
    }
}
