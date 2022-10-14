using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Sounds.Data;

namespace Sounds
{
    /// <summary>
    /// Sound関連の管理クラス
    /// </summary>

    public partial class SoundOperator : MonoBehaviour
    {
        [SerializeField] int _sounderCount = 5;
        [SerializeField, Range(0, 1)] float _masterVolume = 0.5f;
        [SerializeField, Range(0, 1)] float _bgmVolume = 0.5f;
        [SerializeField, Range(0, 1)] float _seVolume = 0.5f;
        [SerializeField] List<SoundDataBase> _soundDataBaseList;

        List<Sounder> _sounderList;

        protected static SoundOperator Instance { get; private set; }

        void Awake()
        {
            Instance = this;

            SoundMasterData.Initalize();

            SoundMasterData.SetMaster(_masterVolume);
            SoundMasterData.SetBGM(_bgmVolume);
            SoundMasterData.SetSE(_seVolume);
        }

        void Start()
        {
            _sounderList = new List<Sounder>();

            CreateSounder();
        }

        /// <summary>
        /// 停止の申請
        /// </summary>
        /// <param name="requester"></param>
        protected void StopRequest(StopRequester requester)
        {
            if (requester.IsAll)
            {
                _sounderList.ForEach(s => s.Initalize());
            }
            else
            {
                try
                {
                    Sounder sounder;

                    foreach (string path in requester.PathList)
                    {
                        sounder = _sounderList.First(s => s.CurrentPath == path);
                        sounder.Initalize();
                    }
                }
                catch
                {
                    Debug.LogWarning("再生終了する際にエラーが起きました。");
                }
            }
        }

        /// <summary>
        /// 再生の申請
        /// </summary>
        /// <param name="requestData"></param>
        protected void PlayRequest(PlayRequestData requestData)
        {
            Sounder sounder;

            try
            {
                sounder = _sounderList.First(s => !s.IsUsing);
            }
            catch
            {
                Debug.LogWarning("Sounderが上限に達したので新たに生成します。");

                CreateSounder();
                PlayRequest(requestData);

                return;
            }

            SoundDataBase dataBase = FindSoundDataBase(requestData.SoundType);
            SoundData data = FindSoundData(dataBase, requestData.Path);

            sounder.Set(data, requestData);
        }

        SoundDataBase FindSoundDataBase(SoundType type)
        {
            try
            {
                return _soundDataBaseList.First(s => s.SoundType == type);
            }
            catch
            {
                Debug.LogWarning($"SoundDataBaseが見つかりませんでした。FindData => {type}");
                return null;
            }
        }

        SoundData FindSoundData(SoundDataBase dataBase, string path)
        {
            try
            {
                return dataBase.GetData(path);
            }
            catch
            {
                Debug.LogWarning($"SoundDataが見つかりませんでした。FindData => {path}");
                return null;
            }
        }

        void CreateSounder()
        {
            for (int index = 0; index < _sounderCount; index++)
            {
                Sounder sounder = new Sounder(transform, Play);
                _sounderList.Add(sounder);
            }
        }

        void Play(IEnumerator enumerator)
        {
            StartCoroutine(enumerator);
        }
    }
}