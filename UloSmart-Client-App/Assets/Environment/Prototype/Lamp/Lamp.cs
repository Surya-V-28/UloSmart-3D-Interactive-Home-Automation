using UnityEngine;
using UnityEngine.Events;

namespace Prototype.Lamp
{
    [AddComponentMenu("Prototype/Lamp/" + nameof(Lamp))]
    public class Lamp : MonoBehaviour
    {
        private void Start()
        {
            material = GetComponent<MeshRenderer>().material;
        }

        public void Toggle()
        {
            ToggleWithoutNotifying();

            onToggle.Invoke(this, isOn);
        }

        public void ToggleWithoutNotifying()
        {
            if (isOn) TurnOffWithoutNotifying();
            else TurnOnWithoutNotifying();
        }

        public void TurnOn()
        {
            TurnOnWithoutNotifying();

            onToggle.Invoke(this, true);            
        }

        public void TurnOnWithoutNotifying()
        {
            material.SetColor("_BaseColor", Color.red);
            isOn = true;
        }

        public void TurnOffWithoutNotifying()
        {
            material.SetColor("_BaseColor", Color.black);
            isOn = false;            
        }

        public void TurnOff()
        {
            TurnOffWithoutNotifying();

            onToggle.Invoke(this, false);
        }

        public void AddOnToggleListener(UnityAction<Lamp, bool> listener)
        {
            onToggle.AddListener(listener);
        }

        public void RemoveOnToggleListener(UnityAction<Lamp, bool> listener)
        {
            onToggle.RemoveListener(listener);
        }

        public bool IsOn => isOn;

        


        private bool isOn = false;

        private Material material = null;

        [SerializeField]
        private UnityEvent<Lamp, bool> onToggle;
    }
}