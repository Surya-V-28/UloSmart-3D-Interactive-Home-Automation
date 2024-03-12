using UnityEngine;
using InteractableSystem;

namespace Prototype.Door
{
    [AddComponentMenu("Prototype/Door/" + nameof(InteractionHandler))]
    public class InteractionHandler : MonoBehaviour
    {
        private void OnEnable()
        {
            if (door == null)
            {
                door = GetComponent<Door>();
                interactable = GetComponent<Interactable>();
            }

            setActionNameFromDoorState();

            door.AddOnToggleListener(onToggle);
        }        

        private void OnDisable()
        {
            door.RemoveOnToggleListener(onToggle);
        }

        private void onToggle(Door door, bool isOpen) => setActionNameFromDoorState();

        private void setActionNameFromDoorState() => interactable.SetActionName((door.IsOpen) ? "Close" : "Open");



        private Door door = null;
        private Interactable interactable = null;
    }
}