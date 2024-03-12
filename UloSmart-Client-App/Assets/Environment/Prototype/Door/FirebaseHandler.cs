using UnityEngine;
using Firebase;
using Firebase.Database;
using System.Threading.Tasks;

namespace Prototype.Door
{
    [AddComponentMenu("Prototype/Door/" + nameof(FirebaseHandler))]
    public class FirebaseHandler : MonoBehaviour
    {
        private void Start()
        {
            door = GetComponent<Door>();
            FirebaseInitializer.Instance.AddOnInitializedListener(onFirebaseInitialized);
        }

        private void OnDestroy()
        {
            door.RemoveOnToggleListener(onToggle);
        }

        private void onFirebaseInitialized(FirebaseApp app)
        {
            door.AddOnToggleListener(onToggle);
            databaseReference.ValueChanged += onDatabaseValueChanged;

        }        

        private void onToggle(Door door, bool isOpen)
        {
            async Task asyncPart()
            {
                await databaseReference.Child("isOpen").SetValueAsync( (isOpen) ? 1 : 0 );
            }

            asyncPart();
        }

        private void onDatabaseValueChanged(object sender, ValueChangedEventArgs args)
        {
            bool isOpen = int.Parse(args.Snapshot.Child("isOpen").GetRawJsonValue()) == 1;

            if (isOpen != door.IsOpen) door.ToggleWithoutNotifying();
        }

        private FirebaseDatabase database => FirebaseDatabase.DefaultInstance;

        private DatabaseReference databaseReference => database.GetReference(databasePath);




        [SerializeField]
        private string databasePath = "";

        private Door door = null;
    }
}