using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GasSystem
{
    public class Gas : MonoBehaviour
    {
        private void OnTriggerEnter(Collider enteredCollider)
        {
            onZoneEntered.Invoke(this, enteredCollider.gameObject);
        }

        public void Emit()
        {
            onToggleEmit.Invoke(this, true);
        }

        public void StopEmitting()
        {
            onToggleEmit.Invoke(this, false);
        }

        public void AddOnZoneEnteredListener(UnityAction<Gas, GameObject> listener) => onZoneEntered.AddListener(listener);

        public void RemoveOnZoneEnteredListener(UnityAction<Gas, GameObject> listener) => onZoneEntered.RemoveListener(listener);

        public void AddOnZoneExitedListener(UnityAction<Gas, GameObject> listener) => onZoneExited.AddListener(listener);

        public void RemoveOnZoneExitedListener(UnityAction<Gas, GameObject> listener) => onZoneExited.RemoveListener(listener);

        public void AddOnToggleEmitListener(UnityAction<Gas, bool> listener) => onToggleEmit.AddListener(listener);

        public void RemoveOnToggleEmitListener(UnityAction<Gas, bool> listener) => onToggleEmit.RemoveListener(listener);



        private UnityEvent<Gas, bool> onToggleEmit;

        private UnityEvent<Gas, GameObject> onZoneEntered;
        private UnityEvent<Gas, GameObject> onZoneExited;
    }
}