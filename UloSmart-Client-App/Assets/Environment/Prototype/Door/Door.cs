using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

namespace Prototype.Door
{
    [AddComponentMenu("Prototype/Door/" + nameof(Door))]
    public partial class Door : MonoBehaviour
    {
        private void Start()
        {
            isOpen = false;
            closedRotation = transform.localEulerAngles;
        }

        public void ToggleWithoutNotifying()
        {
            if (toggleTween.IsActive())
            {
                toggleTween.Kill();
                toggleTween = null;
            }

            if (!isOpen)
            {
                toggleTween = transform.DOLocalRotate(closedRotation, 1.0f);
                isOpen = true;
            }
            else
            {
                toggleTween = transform.DOLocalRotate(openRotation, 1.0f);
                isOpen = false;
            }

            toggleTween.OnComplete(() => toggleTween = null)
            .OnKill(() => toggleTween = null);
        }

        public void Toggle()
        {
            ToggleWithoutNotifying();
            ToggleEvent.Invoke(this, isOpen);
        }

        public void AddOnToggleListener(UnityAction<Door, bool> listener) => ToggleEvent.AddListener(listener);

        public void RemoveOnToggleListener(UnityAction<Door, bool> listener) => ToggleEvent.RemoveListener(listener);

        public bool IsOpen => isOpen;


        private Vector3 closedRotation;
        [SerializeField]
        private Vector3 openRotation;
        private bool isOpen = false;
        private Tween toggleTween = null;

        [SerializeField]
        private UnityEvent<Door, bool> ToggleEvent;        
    }

#if UNITY_EDITOR
    public partial class Door : MonoBehaviour
    {
        [ContextMenu("Toggle")]
        private void toggleMenuItem() => Toggle();
    }
#endif
}