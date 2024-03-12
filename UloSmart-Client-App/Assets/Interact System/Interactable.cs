using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace InteractableSystem
{
    [AddComponentMenu(nameof(InteractableSystem) + "/" + nameof(Interactable))]
    public class Interactable : MonoBehaviour
    {
        private void Start()
        {

        }

        public void Interact(Interactor interactor)
        {
            if (!isHighlighted || !isInteractable) return;
            
            onInteract.Invoke(interactor);
        }

        public void Highlight()
        {
            if (isHighlighted) return;

            highlightIndicatorScreen.Show(objectName, actionName);
            isHighlighted = true;
        }

        public void StopHighlighting()
        {
            if (!isHighlighted) return;

            highlightIndicatorScreen.Hide();
            isHighlighted = false;
        }

        public void BlockInteractions()
        {
            if (!isInteractable) return;

            isInteractable = false;
        }
        public void AllowInteractions()
        {
            if (isInteractable) return;

            isInteractable = true;
        }

        public void SetObjectName(string value)
        {
            objectName = value;
        }

        public void SetActionName(string value)
        {
            actionName = value;

            if (isHighlighted) highlightIndicatorScreen.SetAction(value);            
        }

        public void AddOnInteractListener(UnityAction<Interactor> listener) => onInteract.AddListener(listener);

        public void RemoveOnInteractListener(UnityAction<Interactor> listener) => onInteract.RemoveListener(listener);

        private HighlightIndicatorScreen highlightIndicatorScreen => HighlightIndicatorScreen.Instance;

        internal bool IsHighlighted => isHighlighted;

        internal bool IsInteractable => isInteractable;


        [SerializeField]
        private string objectName = "Interactable";
        [SerializeField]
        private string actionName = "Action";
        private bool isHighlighted = false;
        private bool isInteractable = true;
        [SerializeField]
        private UnityEvent<Interactor> onInteract;        
    }
}