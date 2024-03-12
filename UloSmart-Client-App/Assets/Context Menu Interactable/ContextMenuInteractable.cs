using Cinemachine;
using InteractableSystem;
using PlayerSystem;
using Prototype.Lamp;
using UnityEngine;
using UnityEngine.Events;

namespace ContextMenuSystem
{
    [RequireComponent(typeof(Interactable))]
    public abstract class ContextMenuInteractable<TObject>: MonoBehaviour
    {
        private void OnEnable()
        {
            if (onEnableCount != 0)
            {
                interactable.AddOnInteractListener(onInteract);
            }

            ++onEnableCount;
        }

        private void Start()
        {            
            interactable = GetComponent<Interactable>();
            interactable.SetActionName("Open Actions Menu");

            interactable.AddOnInteractListener(onInteract);

            spectatorCameraDefaultPriority = spectatorCamera.Priority;
        }

        private void onInteract(Interactor interactor)
        {
            interactingPlayer = interactor.GetComponent<Player>();
            interactingPlayer.ToCursorModeState();
            spectatorCamera.Priority = 100;

            contextMenu.Open(GetComponent<TObject>());
            
            contextMenu.AddOnCloseListener(contextMenuOnClose);
        }

        private void contextMenuOnClose(ContextMenu<TObject> contextItem)
        {
            spectatorCamera.Priority = spectatorCameraDefaultPriority;

            interactingPlayer.ExitCursorModeState();
            interactingPlayer = null;
            
            contextMenu.RemoveOnCloseListener(contextMenuOnClose);
        }

        private void OnDisable()
        {
            interactable.RemoveOnInteractListener(onInteract);
        }
        

        private int onEnableCount = 0;
        private Player interactingPlayer = null;
        private int spectatorCameraDefaultPriority = 0;

        private Interactable interactable = null;
        [SerializeField]
        private ContextMenu<TObject> contextMenu = null;
        [SerializeField]
        private CinemachineVirtualCamera spectatorCamera = null;
    }
}
