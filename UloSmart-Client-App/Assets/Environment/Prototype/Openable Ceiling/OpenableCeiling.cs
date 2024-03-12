using UnityEngine;
using UnityEngine.Events;

using DG.Tweening;

namespace OpenableCeilingSystem
{
    [AddComponentMenu("OpenableCeilingSystem/" + nameof(OpenableCeiling))]
    public class OpenableCeiling : MonoBehaviour
    {
        private void Start()
        {
            closedPosition = transform.position;
        }

        public void Open()
        {
            OpenWithoutNotifying();

            onIsOpenChangedListener.Invoke(this, isOpen);
        }

        public void OpenWithoutNotifying()
        {
            if (isOpen) return;

            killMoveTweener();

            moveTweener = transform.DOMove(openPosition, 1.0f).OnComplete(killMoveTweener);
            isOpen = true;
        }        

        public void Close()
        {
            CloseWithoutNotifying();

            onIsOpenChangedListener.Invoke(this, isOpen);
        }

        public void CloseWithoutNotifying()
        {
            if (!isOpen) return;

            killMoveTweener();

            moveTweener = transform.DOMove(closedPosition, 1.0f).OnComplete(killMoveTweener);
            isOpen = false;
        }        

        public void AddOnIsOpenChangedListener(UnityAction<OpenableCeiling, bool> listener) => onIsOpenChangedListener.AddListener(listener);

        public void RemoveOnIsOpenChangedListener(UnityAction<OpenableCeiling, bool> listener) => onIsOpenChangedListener.RemoveListener(listener);

        public bool IsOpen => isOpen;

        private void killMoveTweener()
        {
            if (!moveTweener.IsActive()) return;
            
            moveTweener.Kill();
            moveTweener = null;
        }        




        [SerializeField]
        private float openCloseDuration = 3.0f;
        private bool isOpen = false;
        private Tweener moveTweener = null;

        private Vector3 closedPosition = Vector3.zero;

        private Vector3 openPosition = Vector3.zero;

        private UnityEvent<OpenableCeiling, bool> onIsOpenChangedListener;
    }
}