using UnityEngine;

namespace ContextMenuSystem
{
    public abstract class ContextMenuPage<TContextItem> : MonoBehaviour
    {
        protected virtual void OnEnable()
        {
            menu = GetComponentInParent<ContextMenu<TContextItem>>();
        }

        protected TContextItem contextItem => menu.ContextItem;



        protected ContextMenu<TContextItem> menu = null;
    }
}