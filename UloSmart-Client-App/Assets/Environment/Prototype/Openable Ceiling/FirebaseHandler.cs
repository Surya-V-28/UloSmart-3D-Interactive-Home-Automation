using System.Threading.Tasks;

using UnityEngine;

using Firebase.Database;

namespace OpenableCeilingSystem
{
    [AddComponentMenu("Openable Ceiling System/" + nameof(FirebaseHandler))]
    public class FirebaseHandler : MonoBehaviour
    {
        private void Start()
        {
            ceiling = GetComponent<OpenableCeiling>();

            FirebaseInitializer.Instance.AddOnInitializedListener(
                (app) =>
                {
                    reference.ValueChanged += onDatabaseValueChanged;

                    ceiling.AddOnIsOpenChangedListener(onIsOpenedChangedListener);
                }
            );
        }

        private void onIsOpenedChangedListener(OpenableCeiling ceiling, bool isOpen)
        {
            reference.Child("isOpen").SetValueAsync(isOpen);
        }

        private void onDatabaseValueChanged(object sender, ValueChangedEventArgs eventArgs)
        {
            bool databaseIsOpen = bool.Parse(eventArgs.Snapshot.Child("isOpen").GetRawJsonValue());
            if (databaseIsOpen == ceiling.IsOpen) return;
            
            if (databaseIsOpen) ceiling.Open();
            else ceiling.Close();
        }

        private FirebaseDatabase database => FirebaseDatabase.DefaultInstance;

        private DatabaseReference reference => database.GetReference(databasePath);




        [SerializeField]
        private string databasePath = "";

        private OpenableCeiling ceiling = null;
    }
}