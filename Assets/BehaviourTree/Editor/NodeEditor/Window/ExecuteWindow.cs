using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree.Edit
{
    public class ExecuteWindow : WindowBase
    {
        protected override ViewBase SetGraphView()
        {
            return new ExecuteView();
        }

        
    }
}