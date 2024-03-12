using UnityEngine;
using UnityEngine.UIElements;

using DG.Tweening;

using PlayerSystem;
using RoomSystem;

namespace GasWarningSystem
{
    [AddComponentMenu("Gas Warning System" + "/" + nameof(GasWarningUI))]
    public class GasWarningUI : MonoBehaviour
    {        
        private void OnEnable()
        {
            if (onEnableExecutionCount == 0)
           {
                initialize();

                ++onEnableExecutionCount;
            }

            setupOpenRoomsListButton();            
            setupRoomsList();
        }

        public void UpdateGasList()
        {
            if (gasWarner.rooms.Count != 0)
            {
                if (!gameObject.activeSelf)
                    gameObject.SetActive(true);
                else
                    populateRoomsList();
            }
            else if (gasWarner.rooms.Count == 0 && gameObject.activeSelf)
                gameObject.SetActive(false);
        }

        private void initialize()
        {
            uiDocument = GetComponent<UIDocument>();
        }

        private void setupOpenRoomsListButton()
        {
            VisualElement container = rootElement.Query<VisualElement>("OpenRoomsListButtonContainer");
            
            Button openRoomsListButton = container.Query<Button>("OpenRoomsListButton");
            openRoomsListButton.clicked += toggleRoomsListIsOpen;

            Label exclamation = container.Query<Label>("Exclamation");
            exclamation.RegisterCallback<TransitionEndEvent>(
                (evt) =>
                {
                    Debug.Log("Transition ended");
                    exclamation.ToggleInClassList("scaled");
                }
            );

            rootElement.schedule.Execute(() => exclamation.ToggleInClassList("scaled")).StartingIn(500);
        }

        private void setupRoomsList()
        {
            roomsList = rootElement.Query<VisualElement>("RoomsList");
            populateRoomsList();
        }

        private void populateRoomsList()
        {
            roomsList.Clear();

            foreach (Room room in gasWarner.rooms)
            {
                Button roomsListItem = new Button();

                roomsListItem.AddToClassList("list-item");
                roomsListItem.AddToClassList("hidden");

                roomsListItem.text = room.name;

                roomsListItem.clicked += () =>
                {
                    Debug.Log("Clicked Room List Item");

                    Player player = FindObjectOfType<Player>();
                    player.TeleportToRoom(room);

                    toggleRoomsListIsOpen();
                };

                roomsList.Add(roomsListItem);
            }
        }    

        private void toggleRoomsListIsOpen()
        {
            int i = 0;
            foreach (VisualElement listItem in roomsList.Children())
            {
                rootElement.schedule.Execute(() => listItem.ToggleInClassList("hidden")).ExecuteLater(100 * i);
                ++i;
            }

            // roomsList.style.visibility = (roomsList.style.visibility == Visibility.Hidden) ? Visibility.Visible : Visibility.Hidden;
        }                

        private VisualElement rootElement => uiDocument.rootVisualElement;




        internal GasWarner gasWarner = null;

        private UIDocument uiDocument = null;
        private VisualElement roomsList = null;

        private int onEnableExecutionCount = 0;
    }
}