using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Prototype.Lamp
{
    public class MainPage : MonoBehaviour
    {
        private void OnEnable()
        {
            contextMenu = GetComponentInParent<LightContextMenu>();
            contextLamp = contextMenu.ContextItem;

            uiDocument = GetComponent<UIDocument>();

            getUIElements();

            toggleButton.text = "Turn " + ((contextLamp.IsOn) ? "Off" : "On");
            toggleButton.RegisterCallback<ClickEvent>(onToggleButtonClicked);

            toChangeColorPageButton.RegisterCallback<ClickEvent>(onToChangeColorPageButtonClicked);

            closeWindowButton.RegisterCallback<ClickEvent>(onCloseWindowButtonClicked);
        }
        private void OnDisable()
        {
            toggleButton.UnregisterCallback<ClickEvent>(onToggleButtonClicked);
            toChangeColorPageButton.UnregisterCallback<ClickEvent>(onToChangeColorPageButtonClicked);
            closeWindowButton.UnregisterCallback<ClickEvent>(onCloseWindowButtonClicked);
        }

        private void onToggleButtonClicked(ClickEvent evt)
        {
            contextLamp.Toggle();

            toggleButton.text = "Turn " + ((contextLamp.IsOn) ? "Off" : "On");
        }

        private void onToChangeColorPageButtonClicked(ClickEvent evt) { }       

        private void onCloseWindowButtonClicked(ClickEvent evt)
        {
            contextMenu.Close();
        }

        private void getUIElements()
        {
            toggleButton = root.Query<Button>(name: "ToggleButton");
            toChangeColorPageButton = root.Query<Button>(name: "ToChangeColorPageButton");
            closeWindowButton = root.Query<Button>(name: "CloseWindowButton");
        }

        private VisualElement root => uiDocument.rootVisualElement;




        private Button toggleButton = null;
        private Button toChangeColorPageButton = null;
        private Button closeWindowButton = null;

        private UIDocument uiDocument = null;

        private Lamp contextLamp = null;

        private LightContextMenu contextMenu = null;
    }
}