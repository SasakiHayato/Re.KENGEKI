using UnityEngine;
using UnityEngine.UIElements;

namespace BehaviourTree.Edit
{
    public class ExecuteNode : BaseNode
    {
        public ExecuteNode() : base()
        {
            mainContainer.Add(OpenWindow());
        }

        VisualElement OpenWindow()
        {
            Button button = new Button();
            button.name = "Open";

            button.clicked += () => CreateWindow();

            return button;
        }

        void CreateWindow()
        {
            ExecuteWindow window = ScriptableObject.CreateInstance<ExecuteWindow>();
            window.Show();
            window.titleContent = new GUIContent("_ExecuteWindow");
        }

        protected override string SetTitle()
        {
            return "ExecuteData";
        }
    }
}
