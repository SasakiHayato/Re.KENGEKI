using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using BehaviourTree.Data;

namespace BehaviourTree.Edit
{
    [CustomEditor(typeof(BehaviourTreeUser))]
    public class CustomBehaviourTreeUser : Editor
    {
        string _userName;
        bool _isOpen;

        List<TreeDataBase> _dataList;

        private void OnEnable()
        {
            BehaviourTreeUser user = target as BehaviourTreeUser;

            _userName = user.name;
            _dataList = user.TreeDataBaseList;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("OpenNodeEditor") && !_isOpen)
            {
                _isOpen = true;
                Open();
            }
        }

        void Open()
        {
            TreeWindow window = CreateInstance<TreeWindow>();
            window.Show();
            window.SetCloseAction(() => _isOpen = false);

            window.titleContent = new GUIContent($"{_userName}_TreeWindow");
        }
    }
}
