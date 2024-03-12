using UnityEngine;

using RoomSystem;

public class InteractableObject : MonoBehaviour
{
    private void Start()
    {
        room.ToGassedState();
    }



    private Room room = null;
}
