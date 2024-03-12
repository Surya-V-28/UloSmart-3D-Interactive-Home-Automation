using UnityEngine;
using Firebase.Database;
using System.Threading.Tasks;

namespace Prototype.Lamp
{
    [AddComponentMenu("Prototype/Lamp/" + nameof(FirebaseHandler))]
    public class FirebaseHandler : MonoBehaviour
    {
        private void OnEnable()
        {
            lamp = GetComponent<Lamp>();            
        }

        private void Start()
        {
            FirebaseInitializer.Instance.AddOnInitializedListener(
                (app) =>
                {
                    databaseReference = database.GetReference(databasePath);
                    databaseReference.ValueChanged += onValueChanged;

                    lamp.AddOnToggleListener(onToggle);
                }
            );
        }

        private void OnDisable()
        {
            databaseReference.ValueChanged -= onValueChanged;
        }

        private void onToggle(Lamp lamp, bool isOn)
        {
            async Task asyncPart()
            {
                await databaseReference.Child("isOn").SetValueAsync(isOn);
            }

            asyncPart();
        }

        private void onValueChanged(object sender, ValueChangedEventArgs evt)
        {
            bool databaseIsOn = bool.Parse(evt.Snapshot.Child("isOn").GetRawJsonValue());

            Debug.Log($"Lamp value changed to {databaseIsOn}", this.gameObject);

            if (lamp.IsOn != databaseIsOn) lamp.ToggleWithoutNotifying();
        }

        private FirebaseDatabase database => FirebaseDatabase.DefaultInstance;

        

        private DatabaseReference databaseReference = null;

        [SerializeField]
        private string databasePath = "";
        private Lamp lamp = null;
    }
}