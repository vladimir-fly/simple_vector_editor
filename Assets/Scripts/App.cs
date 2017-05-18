using SVE.Controllers;
using SVE.Models.Projects;
using SVE.UI;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace SVE
{
    public class App : MonoBehaviour
    {
        public MainUI MainUi;

        private void Start()
        {
            Debug.Log("<color=green>[App]</color>" +
                      "<color=blue>[Start]</color>" +
                      "<b><color=#003653> Welcome!</color></b>");

            if (!CheckBindings()) return;

            var controller = new MainController(new SimpleProject());
            Init(controller, MainUi);
        }

        private bool CheckBindings()
        {
            if (MainUi == null) Debug.LogError("[App][CheckBindings] MainUI gameObject not assigned!");
            return MainUi != null;
        }

        private static void Init(MainController mainController, MainUI mainUi)
        {
            mainUi.Undo = mainController.Undo;
            mainUi.Redo = mainController.Redo;
            mainUi.Save = mainController.Save;

            mainUi.OnCreateShape = (tool, color, arg3) =>
            {
//                var shapeType = typeof(IShape);
//                var shapeListType = shapeType.MakeGenericType(type);
//                var shapeInstance = Activator.CreateInstance(shapeListType);
//                var command = new Add(mainController.Project, shapeInstance as Shape);
//
//                mainController.AddCommand(command);
            };

            mainController.CanvasRedrawCallback = mainUi.RedrawCanvas;
        }
    }
}