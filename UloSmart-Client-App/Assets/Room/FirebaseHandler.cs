using UnityEngine;
using Firebase.Database;

namespace RoomSystem
{
    public class FirebaseHandler : MonoBehaviour
    {
        private void Start()
        {
            room = GetComponent<Room>();

            FirebaseInitializer.Instance.AddOnInitializedListener(
                (app) =>
                {
                    databaseReference = database.GetReference(databasePath);
                    databaseReference.ValueChanged += onValueChanged;

                    // Listen to changes in client room and update database
                }
            );
        }

        private void onValueChanged(object sender, ValueChangedEventArgs eventArgs)
        {
            DataSnapshot snapshot = eventArgs.Snapshot;

            bool isGassed = int.Parse(snapshot.Child("mqSensor").Child("isDetected").GetRawJsonValue()) == 0 ? false : true;
            if (isGassed && !room.IsGassed) room.ToGassedState();
            else if (!isGassed && room.IsGassed) room.ExitGassedState();
        }

        private FirebaseDatabase database => FirebaseDatabase.DefaultInstance;




        [SerializeField]
        private string databasePath = "";

        private Room room = null;

        private DatabaseReference databaseReference = null;
    }
}