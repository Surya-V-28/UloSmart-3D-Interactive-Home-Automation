using UnityEngine;
using Firebase;
using System.Threading.Tasks;
using UnityEngine.Events;

[DefaultExecutionOrder(-1000)]
public class FirebaseInitializer : MonoBehaviour
{
    public void Start()
    {
        instance = this;
        checkDependencies();
    }
    
    private async Task checkDependencies()
    {
        DependencyStatus dependencyStatus = await FirebaseApp.CheckAndFixDependenciesAsync();

        if (dependencyStatus == DependencyStatus.Available)
        {
            Debug.Log("Firebase dependencies are setup correctly");
            onInitialized.Invoke(FirebaseApp.DefaultInstance);
        }
        else
        {
            Debug.LogError($"Firebase depedencies are not setup correctly, status = ${dependencyStatus}");
        }
    }

    public void AddOnInitializedListener(UnityAction<FirebaseApp> listener) => onInitialized.AddListener(listener);

    public void RemoveOnInitializedListener(UnityAction<FirebaseApp> listener) => onInitialized.RemoveListener(listener);



    public static FirebaseInitializer Instance => instance;



    [SerializeField]
    private UnityEvent<FirebaseApp> onInitialized;

    private static FirebaseInitializer instance = null;
}