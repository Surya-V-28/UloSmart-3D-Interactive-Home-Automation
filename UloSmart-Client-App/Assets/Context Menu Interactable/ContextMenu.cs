using UnityEngine;
using UnityEngine.Events;

namespace ContextMenuSystem
{
    [DefaultExecutionOrder(+100)]
    public abstract class ContextMenu<TContextItem> : MonoBehaviour
    {
        public virtual void Start()
        {
            gameObject.SetActive(false);
        }

        public virtual void Open(TContextItem contextItem)
        {
            this.contextItem = contextItem;
            gameObject.SetActive(true);
        }

        public virtual void Close()
        {
            gameObject.SetActive(false);
            onClose.Invoke(this);
        }

        public void AddOnCloseListener(UnityAction<ContextMenu<TContextItem>> listener) => onClose.AddListener(listener);

        public void RemoveOnCloseListener(UnityAction<ContextMenu<TContextItem>> listener) => onClose.RemoveListener(listener);

        public TContextItem ContextItem => contextItem;



        [SerializeField]
        private UnityEvent<ContextMenu<TContextItem>> onClose;

        protected TContextItem contextItem;
    }
}