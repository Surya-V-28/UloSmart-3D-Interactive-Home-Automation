using UnityEngine;
using UnityEngine.Events;

namespace Prototype.TubeLight
{
    [AddComponentMenu("Prototype/" + nameof(TubeLight) + "/" + nameof(TubeLight))]
    public class TubeLight : MonoBehaviour
    {
        private void Start()
        {
            tubeMaterial = GetComponent<MeshRenderer>().materials[1];
            light = GetComponentInChildren<Light>();
            defaultIntensity = light.intensity;

            if (isOn) TurnOn();
            else TurnOff();
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
            tubeMaterial.SetColor("_EmissiveColor", Color.white * 30.0f);
            light.intensity = defaultIntensity;
            isOn = true;
        }

        public void TurnOffWithoutNotifying()
        {            
            tubeMaterial.SetColor("_EmissiveColor", Color.white * 0.3f);
            light.intensity = defaultIntensity * 0.05f;
            isOn = false;
        }

        public void TurnOff()
        {
            TurnOffWithoutNotifying();

            onToggle.Invoke(this, false);
        }

        public void AddOnToggleListener(UnityAction<TubeLight, bool> listener)
        {
            onToggle.AddListener(listener);
        }

        public void RemoveOnToggleListener(UnityAction<TubeLight, bool> listener)
        {
            onToggle.RemoveListener(listener);
        }

        public bool IsOn => isOn;


        [SerializeField]
        private bool isOn = false;

        private Material tubeMaterial = null;
        private new Light light = null;

        private float defaultIntensity = 0.0f;

        [SerializeField]
        private UnityEvent<TubeLight, bool> onToggle;
    }
}