using System.Collections;
using System;
using UnityEngine;
using Sounds.Data;

namespace Sounds
{
    public partial class SoundOperator : MonoBehaviour
    {
        /// <summary>
        /// Soundを鳴らすインスタンス
        /// </summary>

        class Sounder
        {
            float _volume;
            SoundType _soundType;

            GameObject _obj;
            Transform _parent;
            AudioSource _audioSource;
            Action<IEnumerator> _action;

            public string CurrentPath { get; private set; }
            public bool IsUsing { get; private set; }

            public Sounder(Transform parent, Action<IEnumerator> action)
            {
                _obj = new GameObject("Sounder");
                _obj.transform.SetParent(parent);

                _volume = 0;
                _parent = parent;
                _audioSource = _obj.AddComponent<AudioSource>();

                _action = action;
            }

            /// <summary>
            /// データのセット
            /// </summary>
            /// <param name="data"></param>
            /// <param name="requestData"></param>
            public void Set(SoundData data, PlayRequestData requestData)
            {
                _volume = data.Volume;
                _soundType = requestData.SoundType;

                _obj.transform.SetParent(requestData.User);

                _audioSource.clip = data.AudioClip;
                _audioSource.spatialBlend = data.SpatialBlend;
                _audioSource.volume = SetVolume();

                if (requestData.SoundType == SoundType.BGM)
                {
                    _audioSource.loop = true;
                    SoundMasterData.SetBGMPath(data.Path);
                }
                else
                {
                    _audioSource.loop = false;
                }

                CurrentPath = data.Path;
                IsUsing = true;

                _action.Invoke(Play());
            }

            float SetVolume()
            {
                float volume = SoundMasterData.MasterVolume * _volume;

                switch (_soundType)
                {
                    case SoundType.BGM: volume *= SoundMasterData.BGMVolume;
                        break;
                    case SoundType.SE: volume *= SoundMasterData.SEVolume;
                        break;
                }

                return volume;
            }

            /// <summary>
            /// 疑似Update関数
            /// </summary>
            /// <returns></returns>
            IEnumerator Play()
            {
                _audioSource.Play();

                while (_audioSource.isPlaying)
                {
                    _audioSource.volume = SetVolume();
                    yield return null;
                }

                Initalize();
            }

            /// <summary>
            /// 初期化
            /// </summary>
            public void Initalize()
            {
                _volume = 0;

                _audioSource.clip = null;
                _audioSource.spatialBlend = 0;

                _obj.transform.SetParent(_parent);

                CurrentPath = "";
                IsUsing = false;
            }
        }
    }
}
