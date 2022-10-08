using UnityEngine;
using UnityEditor.Experimental.GraphView;
using System.Collections.Generic;

namespace BehaviourTree.Edit
{
    public class ContextualMenuWindow : ScriptableObject, ISearchWindowProvider
    {
        ViewBase _view;

        public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
        {
            return null;
        }

        public void Initialize(ViewBase view)
        {
            _view = view;
        }

        public bool OnSelectEntry(SearchTreeEntry SearchTreeEntry, SearchWindowContext context)
        {
            return false;
        }
    }
}