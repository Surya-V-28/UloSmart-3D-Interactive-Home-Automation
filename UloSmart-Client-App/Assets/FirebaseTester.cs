using System.Threading.Tasks;
using UnityEngine;
using Firebase;
using Firebase.Database;

public class FirebaseTester : MonoBehaviour
{
    private void Start()
    {
        checkDependencies();
    }

    private async Task checkDependencies()
    {
        DependencyStatus status = await FirebaseApp.CheckAndFixDependenciesAsync();

        if (status == DependencyStatus.Available)
        {
            Debug.Log("Depedencies are setup correctly");

            setupValueChangedListener();
        }
        else
        {
            Debug.LogError("Dependencies check failed");
        }
    }

    private void setupValueChangedListener()
    {
        FirebaseDatabase database = FirebaseDatabase.DefaultInstance;

        database.GetReference("hall/light/isOn").ValueChanged += onValueChanged;
    }

    private void onValueChanged(object sender, ValueChangedEventArgs eventArgs)
    {
        int value = int.Parse(eventArgs.Snapshot.GetRawJsonValue());
        Debug.Log($"hall/light/isOn value changed to {value}");
    }
}