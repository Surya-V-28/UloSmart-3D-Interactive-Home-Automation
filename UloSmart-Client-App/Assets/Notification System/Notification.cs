using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace NotificationSystem
{
    [RequireComponent(typeof(Notification))]
    public class Notification : MonoBehaviour
    {
        private void Start()
        {
            uiDocument = GetComponent<UIDocument>();
            getViews();
            
            instance = this;

        }        

        public void Show(string text)
        {
            messageLabel.text = text;
            notificationContainer.style.scale = new StyleScale(new Vector2(1.0f, 0.0f));
        }

        private void getViews()
        {
            notificationContainer = rootVisualElement.Query<VisualElement>("notification");
            messageLabel = rootVisualElement.Query<Label>("messageLabel");
        }

        private VisualElement rootVisualElement => uiDocument.rootVisualElement;
        

        public static Notification Instance => instance;



        private UIDocument uiDocument = null;
        private VisualElement notificationContainer = null;
        private Label messageLabel = null;

        public static Notification instance = null;
    }
}