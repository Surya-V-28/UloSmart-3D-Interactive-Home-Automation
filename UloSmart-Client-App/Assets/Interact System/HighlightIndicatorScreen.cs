using TMPro;
using UnityEngine;

namespace InteractableSystem
{    
    public class HighlightIndicatorScreen : MonoBehaviour
    {
        private void Start()
        {
            instance = this;

            objectLabel.SetText("");

            gameObject.SetActive(false);
        }

        public void Show(string objectName, string actionName)
        {
            gameObject.SetActive(true);
            objectLabel.SetText(objectName);
            actionLabel.SetText(actionName);
        }

        public void SetAction(string action) => actionLabel.SetText(action);

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public static HighlightIndicatorScreen Instance => instance;





        [SerializeField]
        private TMP_Text objectLabel = null;
        [SerializeField]
        private TMP_Text actionLabel = null;


        private static HighlightIndicatorScreen instance = null;
    }
}