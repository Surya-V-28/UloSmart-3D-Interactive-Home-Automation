using UnityEngine;
using Firebase.Database;
using Firebase;
using System.Threading.Tasks;

namespace Prototype.Fan
{
    [AddComponentMenu("Prototype/Fan/FirebaseHandler")]
    public class FirebaseHandler : MonoBehaviour
    {
        private void Start()
        {
            fan = GetComponent<Fan>();
            FirebaseInitializer.Instance.AddOnInitializedListener(onFirebaseInitialized);
        }

        private void onFirebaseInitialized(FirebaseApp app)
        {
            databaseReference.ValueChanged += onValueChanged;
            fan.AddOnToggleListener(onToggled);
        }

        private void onValueChanged(object sender, ValueChangedEventArgs args)
        {
            DataSnapshot dataSnapshot = args.Snapshot;

            bool isOn = int.Parse(dataSnapshot.Child("isOn").GetRawJsonValue()) == 1;
            if (isOn != fan.IsOn)
            {
                fan.ToggleWithoutNotifying();
            }
        }

        private void onToggled(Fan fan, bool isOn)
        {
            async Task asyncPart()
            {
                await databaseReference.Child("isOn").SetValueAsync( (isOn) ? 1 : 0 );
            }

            asyncPart();
        }

        private FirebaseDatabase database => FirebaseDatabase.DefaultInstance;
        private DatabaseReference databaseReference => database.GetReference(databasePath);



        [SerializeField]
        private string databasePath;

        private Fan fan;
    }
}