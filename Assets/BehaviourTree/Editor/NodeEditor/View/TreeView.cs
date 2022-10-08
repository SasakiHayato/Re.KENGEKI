using UnityEngine;
using UnityEditor.Experimental.GraphView;

namespace BehaviourTree.Edit
{
    public class TreeView : ViewBase
    {
        public TreeView()
        {
            ContextualMenuWindow window = ScriptableObject.CreateInstance<ContextualMenuWindow>();
            window.Initialize(this);

            nodeCreationRequest += context =>
            {
                SearchWindow.Open(new SearchWindowContext(context.screenMousePosition), window);
            };
        }
    }
}