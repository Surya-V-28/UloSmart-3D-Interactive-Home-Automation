using UnityEngine;
using UnityEngine.UIElements;
using ContextMenuSystem;

namespace Prototype.Fan
{
    [AddComponentMenu("Prototype/Fan/Context Menu/MainPage")]
    public class MainPage : ContextMenuPage<Fan> 
    {
        protected override void OnEnable()
        {
            base.OnEnable();

            if (contextItem == null) return;

            uiDocument = GetComponent<UIDocument>();

            getUIElements();

            closeWindowButton.clicked += onCloseWindowButtonClicked;
            toggleButton.clicked += onToggleButtonClicked;

            setToggleButtonText(contextItem.IsOn);
            contextItem.AddOnToggleListener(onFanToggled);
        }

        private void OnDisable()
        {
            if (contextItem == null) return;

            contextItem.RemoveOnToggleListener(onFanToggled);
        }

        private void onCloseWindowButtonClicked() => menu.Close();

        private void onToggleButtonClicked() => contextItem.Toggle();

        private void onFanToggled(Fan fan, bool isOn)
        {
            setToggleButtonText(isOn);
        }

        private void setToggleButtonText(bool isOn) => toggleButton.text = (isOn) ? "TURN OFF" : "TURN ON";

        private void getUIElements()
        {
            closeWindowButton = root.Query<Button>(name: "CloseWindowButton");
            toggleButton = root.Query<Button>(name: "ToggleButton");
        }        

        private VisualElement root => uiDocument.rootVisualElement;



        private Button toggleButton = null;
        private Button closeWindowButton = null;

        private UIDocument uiDocument = null;
    }
}