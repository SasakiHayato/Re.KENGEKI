using UnityEngine;
using BehaviourTree.Node;

using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace BehaviourTree.Data
{
    /// <summary>
    /// AI行動のデータクラス
    /// </summary>
    [System.Serializable]
    public class ExecuteData
    {
        [SerializeField] ExecuteType _treeExecuteType;
        [SerializeField] ConditionalNode _condition;
        [SerializeField] ActionNode _action;

        public ExecuteType TreeExecuteType => _treeExecuteType;
        public ConditionalNode Condition => _condition;
        public ActionNode Action => _action;

        public ExecuteData Copy()
        {
            MemoryStream stream = new MemoryStream();
            BinaryFormatter binary = new BinaryFormatter();

            binary.Serialize(stream, this);
            stream.Position = 0;

            return (ExecuteData)binary.Deserialize(stream);
        }
    }
}