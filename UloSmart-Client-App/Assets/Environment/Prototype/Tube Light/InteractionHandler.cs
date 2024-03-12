using UnityEngine;
using InteractableSystem;
using Google.MiniJSON;

namespace Prototype.TubeLight
{
    [AddComponentMenu("Prototype/TubeLight/InteractionHandler")]
    public class InteractionHandler : MonoBehaviour
    {
        private void OnEnable()
        {
            if (tubeLight == null)
            {
                tubeLight = GetComponent<TubeLight>();
                interactable = GetComponent<Interactable>();
            }

            setInteractableNameBasedOnToggleStatus();

            setupListeners();
        }

        private void OnDisable()
        {
            tubeLight.RemoveOnToggleListener(onToggle);
        }

        private void setupListeners()
        {
            tubeLight.AddOnToggleListener(onToggle);
        }

        private void onToggle(TubeLight tubeLight, bool isOn)
        {
            setInteractableNameBasedOnToggleStatus();
        }

        private void setInteractableNameBasedOnToggleStatus() => interactable.SetActionName((tubeLight.IsOn) ? "Turn Off" : "Turn On");




        private TubeLight tubeLight = null;
        private Interactable interactable = null;
    }
}