namespace BehaviourTree.Edit
{
    using UnityEditor.Experimental.GraphView;

    public abstract class BaseNode : Node
    {
        public BaseNode()
        {
            title = SetTitle();
        }

        protected abstract string SetTitle();
    }
}