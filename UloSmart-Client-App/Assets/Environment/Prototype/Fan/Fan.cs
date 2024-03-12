using UnityEngine;
using UnityEngine.Events;

namespace Prototype.Fan
{
    [AddComponentMenu("Prototype/Fan/" + nameof(Fan))]
    public partial class Fan : MonoBehaviour
    {
        private void Update()
        {
            if (isOn)
            {
                angularVelocity = Mathf.Clamp(angularVelocity + angularAcceleration * Time.deltaTime, 0.0f, maxAngularVelocity);
            }
            else
            {
                angularVelocity = Mathf.Clamp(angularVelocity - angularAcceleration * Time.deltaTime, 0.0f, maxAngularVelocity);
            }

            if (angularVelocity == 0.0f) return;

            Vector3 rotationDelta = new Vector3(0.0f, angularVelocity, 0.0f) * Time.deltaTime;
            transform.Rotate(rotationDelta);
        }

        public void ToggleWithoutNotifying() => isOn = !isOn;

        public void Toggle()
        {
            ToggleWithoutNotifying();
            ToggleEvent.Invoke(this, isOn);
        }


        public void AddOnToggleListener(UnityAction<Fan, bool> listener) => ToggleEvent.AddListener(listener);
        
        public void RemoveOnToggleListener(UnityAction<Fan, bool> listener) => ToggleEvent.RemoveListener(listener);

        public bool IsOn => isOn;



        private bool isOn = false;

        [SerializeField]
        private float angularAcceleration = 20.0f;
        [SerializeField]
        private float angularDeceleration = 30.0f;

        [SerializeField]
        private float[] speeds;

        private float angularVelocity = 0.0f;
        [SerializeField]
        private float maxAngularVelocity = 90.0f;

        [SerializeField]
        private UnityEvent<Fan, bool> ToggleEvent;
    }

#if UNITY_EDITOR
    public partial class Fan : MonoBehaviour
    {
        [ContextMenu("Toggle")]
        private void ToggleContextMenuItem() => Toggle();
    }
#endif
}