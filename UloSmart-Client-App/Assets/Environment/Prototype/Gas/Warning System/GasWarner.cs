using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

using RoomSystem;

namespace GasWarningSystem
{
    [AddComponentMenu("Gas Warning System/" + nameof(GasWarner))]
    public partial class GasWarner : MonoBehaviour
    {
        private void Start()
        {
            rooms = new List<Room>();

            warningUI = GetComponentInChildren<GasWarningUI>(true);
            warningUI.gasWarner = this;

            alarm = GetComponentInChildren<Alarm>();

            Room[] allRooms = (Room[]) FindObjectsOfType(typeof(Room));
            foreach (Room room in allRooms)
            {
                room.AddOnIsGassedChangedListener(onRoomIsGassedChanged);
            }
        }

        private void onRoomIsGassedChanged(Room room, bool isGassed)
        {
            if (isGassed) AddRoom(room);
            else RemoveRoom(room);
        }

        public void AddRoom(Room room)
        {
            if (rooms.Contains(room)) return;

            rooms.Add(room);
            
            warningUI.UpdateGasList();

            // Send warning notification to phone

            if (!alarm.IsPlaying) alarm.StartAlarm();            

            onRoomsChanged.Invoke(rooms);
        }

        public void RemoveRoom(Room room)
        {
            if (!rooms.Contains(room)) return;

            rooms.Remove(room);
            warningUI.UpdateGasList();

            if (rooms.Count == 0) alarm.Stop();

            onRoomsChanged.Invoke(rooms);
        }

        public void AddOnRoomsChangedListener(UnityAction<List<Room>> listener) => onRoomsChanged.AddListener(listener);

        public void RemoveOnRoomsChangedListener(UnityAction<List<Room>> listener) => onRoomsChanged.RemoveListener(listener);



        internal List<Room> rooms = null;

        [SerializeField]
        private UnityEvent<List<Room>> onRoomsChanged;

        private GasWarningUI warningUI = null;
        private Alarm alarm = null;
    }
}