using InteractableSystem;
using UnityEngine;

namespace Prototype.Lamp
{
    [AddComponentMenu("Prototype/Lamp/" + nameof(InteractionHandler))]
    public class InteractionHandler : MonoBehaviour
    {
        private void Start()
        {
            lamp = GetComponent<Lamp>();
            interactable = GetComponent<Interactable>();

            setInteractableNameBasedOnLampState(lamp.IsOn);

            lamp.AddOnToggleListener(onToggled);
        }

        private void onToggled(Lamp lamp, bool isOn)
        {
            setInteractableNameBasedOnLampState(isOn);
        }

        private void setInteractableNameBasedOnLampState(bool isOn)
        {
            if (isOn) interactable.SetActionName("Turn Off");
            else interactable.SetActionName("Turn On");
        }


        private Lamp lamp = null;
        private Interactable interactable = null;
    }
}