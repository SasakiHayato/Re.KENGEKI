using System;

namespace BehaviourTree.Edit
{
    public interface ILoad
    {
        void Load(Type type);
    }

    public interface ISave
    {
        void Save();
    }
}