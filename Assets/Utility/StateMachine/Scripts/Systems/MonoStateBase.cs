using UnityEngine;
using System;
using MonoState.Data;

namespace MonoState.State
{
    public abstract class MonoStateBase
    {
        public UserRetentionData UserRetentionData { get; set; }

        /// <summary>
        /// ������
        /// </summary>
        public abstract void Setup();

        /// <summary>
        /// �X�e�[�g�ɓ��邽�тɌĂ΂��
        /// </summary>
        public abstract void OnEnable();
        public abstract void Execute();
        public abstract Enum Exit();
    }
}