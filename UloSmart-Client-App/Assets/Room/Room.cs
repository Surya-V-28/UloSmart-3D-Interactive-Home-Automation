using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.VFX;

namespace RoomSystem
{
    public partial class Room : MonoBehaviour
    {
        private void Start()
        {
            gasVisualEffect.gameObject.SetActive(false);
        }

        public void AddOnIsOccupiedChangedListener(UnityAction<Room, bool> listener) => onIsOccupiedChanged.AddListener(listener);

        public void RemoveOnIsOccupiedChangedListener(UnityAction<Room, bool> listener) => onIsOccupiedChanged.RemoveListener(listener);

        public void AddOnIsGassedChangedListener(UnityAction<Room, bool> listener) => onIsGassedChanged.AddListener(listener);

        public void RemoveOnIsGassedChangedListener(UnityAction<Room, bool> listener) => onIsGassedChanged.RemoveListener(listener);

        public string Name => name;

        public bool IsOccupied => isOccupied;

        public bool IsGassed => isGassed;

        public Vector3 TeleportPoint => teleportPointObject.transform.position;

        internal void ToGassedState()
        {
            if (isGassed) return;

            if (disableGasWithDelayCoroutine != null)
            {
                StopCoroutine(disableGasWithDelayCoroutine);
                disableGasWithDelayCoroutine = null;
            }

            gasVisualEffect.gameObject.SetActive(true);
            gasVisualEffect.Reinit();
            isGassed = true;

            onIsGassedChanged.Invoke(this, true);
        }

        internal void ExitGassedState()
        {
            if (!isGassed) return;

            gasVisualEffect.Stop();
            disableGasWithDelayCoroutine = StartCoroutine(disableGasWithDelay());
            isGassed = false;

            onIsGassedChanged.Invoke(this, false);
        }

        IEnumerator disableGasWithDelay()
        {
            yield return new WaitForSeconds(GAS_LIFETIME);
            
            gasVisualEffect.gameObject.SetActive(false);

            disableGasWithDelayCoroutine = null;
        }



        [SerializeField]
        private new string name = "Room";

        private bool isGassed = false;
        [SerializeField]
        private VisualEffect gasVisualEffect = null;
        private Coroutine disableGasWithDelayCoroutine = null;

        private bool isOccupied = false;

        [SerializeField]
        private GameObject teleportPointObject = null;

        [SerializeField]
        private UnityEvent<Room, bool> onIsOccupiedChanged;
        [SerializeField]
        private UnityEvent<Room, bool> onIsGassedChanged;


        private const float GAS_LIFETIME = 30.0f;
    }

#if UNITY_EDITOR
    public partial class Room : MonoBehaviour
    {
        [ContextMenu("To Gassed State")]
        private void ToGassedStateContextMenu()
        {
            if (!Application.isPlaying) return;

            ToGassedState();
        }

        [ContextMenu("Exit Gassed State")]
        private void ExitGassedStateContextMenu()
        {
            if (!Application.isPlaying) return;

            ExitGassedState();
        }
    }
#endif
}