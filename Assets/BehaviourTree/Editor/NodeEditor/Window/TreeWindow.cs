using System;

namespace BehaviourTree.Edit
{
    public class TreeWindow : WindowBase
    {
        Action _action;

        protected override ViewBase SetGraphView()
        {
            return new TreeView();
        }

        public void SetCloseAction(Action action)
        {
            _action = action;
        }

        protected override void DestoryEvent()
        {
            base.DestoryEvent();
            _action.Invoke();
        }
    }
}