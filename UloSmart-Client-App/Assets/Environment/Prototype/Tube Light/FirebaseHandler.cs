using UnityEngine;
using Firebase.Database;
using System.Threading.Tasks;

namespace Prototype.TubeLight
{
    [AddComponentMenu("Prototype/TubeLight/" + nameof(FirebaseHandler))]
    public class FirebaseHandler : MonoBehaviour
    {
        private void OnEnable()
        {
            tubeLight = GetComponent<TubeLight>();
        }

        private void Start()
        {
            FirebaseInitializer.Instance.AddOnInitializedListener(
                (app) =>
                {
                    databaseReference = database.GetReference(databasePath);
                    databaseReference.ValueChanged += onValueChanged;

                    tubeLight.AddOnToggleListener(onToggle);
                }
            );
        }

        private void OnDisable()
        {
            databaseReference.ValueChanged -= onValueChanged;
        }

        private void onToggle(TubeLight lamp, bool isOn)
        {
            async Task asyncPart()
            {
                await databaseReference.Child("isOn").SetValueAsync( (isOn) ? 1 : 0 );
            }

            asyncPart();
        }

        private void onValueChanged(object sender, ValueChangedEventArgs evt)
        {
            bool databaseIsOn = int.Parse(evt.Snapshot.Child("isOn").GetRawJsonValue()) == 1;            

            if (tubeLight.IsOn != databaseIsOn) tubeLight.ToggleWithoutNotifying();
        }

        private FirebaseDatabase database => FirebaseDatabase.DefaultInstance;



        private DatabaseReference databaseReference = null;

        [SerializeField]
        private string databasePath = "";
        private TubeLight tubeLight = null;
    }
}