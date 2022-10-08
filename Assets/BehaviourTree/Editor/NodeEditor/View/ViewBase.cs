using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;

namespace BehaviourTree.Edit
{
    public abstract class ViewBase : GraphView, ISave
    {
        public ViewBase()
        {
            style.flexGrow = 1;
            style.flexShrink = 1;

            SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
            Insert(0, new GridBackground());

            this.AddManipulator(new SelectionDragger());
        }

        public void Save()
        {
            
        }
    }
}
