using UnityEngine;
using UnityEngine.Events;

namespace InteractableSystem
{
    [AddComponentMenu(nameof(InteractableSystem) + "/" + nameof(Interactor))]
    public class Interactor : MonoBehaviour
    {
        private void Update()
        {
            Ray ray = new Ray(camera.transform.position, camera.transform.forward);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, interactableDistance, mask))
            {
                if (hitInfo.transform.TryGetComponent<Interactable>(out Interactable interactable))
                {
#if UNITY_EDITOR
                    Debug.DrawRay(ray.origin, ray.direction * (ray.origin - hitInfo.point).magnitude, Color.green);
#endif

                    if (interactable.IsInteractable && !interactable.IsHighlighted)
                    {
                        interactable.Highlight();
                        highlightedInteractable = interactable;
                    }
                }
                else
                {
                    if (highlightedInteractable != null) StopHighlighting();
                }
            }
            else
            {
                if (highlightedInteractable != null) StopHighlighting();

#if UNITY_EDITOR
                Debug.DrawRay(ray.origin, ray.direction * interactableDistance, Color.red);
#endif
            }
        }

        private void OnDisable()
        {
            if (highlightedInteractable == null) return;

            StopHighlighting();
        }

        public void Interact()
        {
            if (highlightedInteractable == null) return;
            
            highlightedInteractable.Interact(this);
            OnInteract.Invoke(highlightedInteractable);
        }

        public void StopHighlighting()
        {
            highlightedInteractable.StopHighlighting();
            highlightedInteractable = null;
        }        

        public Interactable HighlightedInteractable => highlightedInteractable;        




        [SerializeField]
        private float interactableDistance = 5.0f;
        [SerializeField]
        private LayerMask mask;
        private Interactable highlightedInteractable = null;

        [SerializeField]
        public UnityEvent<Interactable> OnInteract;



        [SerializeField]
        private new Camera camera;
    }
}