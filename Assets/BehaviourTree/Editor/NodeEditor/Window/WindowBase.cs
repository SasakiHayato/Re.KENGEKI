using UnityEditor;

namespace BehaviourTree.Edit
{
    public abstract class WindowBase : EditorWindow
    {
        ViewBase _view;

        void OnEnable()
        {
            _view = SetGraphView();
            rootVisualElement.Add(_view);
        }

        protected abstract ViewBase SetGraphView();

        protected virtual void DestoryEvent() { }

        protected void OnDestroy()
        {
            _view.Save();

            DestoryEvent();
        }
    }
}
