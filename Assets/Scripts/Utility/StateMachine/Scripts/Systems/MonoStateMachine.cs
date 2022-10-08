using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace MonoState
{
    using MonoState.State;
    using MonoState.Data;

    /// <summary>
    /// ステートマシン管理クラス
    /// </summary>
    /// <typeparam name="User">使用者</typeparam>

    public class MonoStateMachine<User> where User : MonoBehaviour
    {
        User _user;

        bool _isRun;

        Dictionary<string, MonoStateBase> _stateDic;
        CurrentMonoStateData _currentMonoState;

        UserRetentionData _userRetentionData;

        // 現在のステート保持クラス
        class CurrentMonoStateData
        {
            public string Path { get; private set; }
            public MonoStateBase MonoState { get; private set; }

            public void SetPath(string path)
            {
                Path = path;
            }

            public void SetMonoState(MonoStateBase state)
            {
                MonoState = state;
            }
        }
        
        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="user"></param>
        public void Initalize(User user)
        {
            _user = user;
            _stateDic = new Dictionary<string, MonoStateBase>();

            StateOperator stateOperator = user.gameObject.AddComponent<StateOperator>();
            stateOperator.Setup(() => Run());

            _currentMonoState = new CurrentMonoStateData();
            _userRetentionData = new UserRetentionData(user.gameObject);
        }

        /// <summary>
        /// ステートの追加
        /// </summary>
        /// <param name="state"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public MonoStateMachine<User> AddState(MonoStateBase state, Enum path)
        {
            _stateDic.Add(path.ToString(), state);
            
            return this;
        }

        /// <summary>
        /// 保持データの保存
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public MonoStateMachine<User> SetData(IRetentionData data)
        {
            _userRetentionData.SetRetentionData(data);

            return this;
        }

        /// <summary>
        /// ステートを回す申請
        /// </summary>
        /// <param name="path"></param>
        public void SetRunRequest(Enum path)
        {
            MonoStateBase monoState = _stateDic.First(d => d.Key == path.ToString()).Value;
            
            _currentMonoState.SetMonoState(monoState);
            _currentMonoState.SetPath(path.ToString());

            foreach (MonoStateBase state in _stateDic.Values)
            {
                state.UserRetentionData = _userRetentionData;
                state.Setup();
            }

            monoState.OnEnable();

            _isRun = true;
        }

        void Run()
        {
            if (!_isRun)
            {
                Debug.Log($"RunRequestがされていません。対象 => {_user.name}");
                return;
            }

            // 次ステートの取得
            string path = _currentMonoState.MonoState.Exit().ToString();

            if (_currentMonoState.Path == path)
            {
                _currentMonoState.MonoState.Execute();
            }
            else
            {
                ChangeState(path);
            }
        }

        /// <summary>
        /// ステートの変更
        /// </summary>
        /// <param name="path"></param>
        public void ChangeState(string path)
        {
            MonoStateBase monoState = _stateDic.First(d => d.Key == path).Value;

            _currentMonoState.SetMonoState(monoState);
            _currentMonoState.SetPath(path);

            _currentMonoState.MonoState.OnEnable();
        }
    }
}