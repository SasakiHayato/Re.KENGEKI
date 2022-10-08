using UnityEngine;

namespace BehaviourTree.Execute
{
    /// <summary>
    /// Action���쐬����ۂ̊��N���X�B
    /// ���̃N���X��h������AI�s�����쐬����B
    /// </summary>
    [System.Serializable]
    public abstract class BehaviourAction : ExecuteBase
    {
        public override void BaseSetup(GameObject user) => Setup(user);

        public override void BaseInit() => Initialize();

        protected virtual void Setup(GameObject user) { }

        protected abstract bool Execute();

        protected override bool BaseExecute() => Execute();

        protected virtual void Initialize() { }
    }
}