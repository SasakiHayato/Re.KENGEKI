using System.Collections.Generic;
using UnityEngine;
using Sounds.Data;

namespace Sounds
{
    public partial class SoundOperator : MonoBehaviour
    {
        /// <summary>
        /// 停止する際のトークン
        /// </summary>

        public struct StopRequester
        {
            bool _isCreate;

            public bool IsAll { get; private set; }
            public List<string> PathList { get; private set; }

            /// <summary>
            /// トークンの生成
            /// </summary>
            /// <returns></returns>
            public StopRequester CreateData()
            {
                _isCreate = true;
                IsAll = false;
                PathList = new List<string>();
                return this;
            }

            /// <summary>
            /// 現在再生中のBGMを止める
            /// </summary>
            /// <returns></returns>
            public StopRequester CurrentBGM()
            {
                if (!_isCreate)
                {
                    Debug.LogWarning("データが生成されていません。");
                }

                foreach (string bgm in SoundMasterData.BGMList)
                {
                    PathList.Add(bgm);
                }

                SoundMasterData.InitalizeList();

                return this;
            }

            /// <summary>
            /// 指定されたSoundを止める
            /// </summary>
            /// <param name="path"></param>
            /// <returns></returns>
            public StopRequester SetPath(string path)
            {
                if (!_isCreate)
                {
                    Debug.LogWarning("データが生成されていません。");
                }

                PathList.Add(path);

                return this;
            }

            /// <summary>
            /// 全て止める
            /// </summary>
            /// <returns></returns>
            public StopRequester SetAll()
            {
                IsAll = true;

                return this;
            }

            /// <summary>
            /// 申請
            /// </summary>
            public void Request()
            {
                Instance.StopRequest(this);
            }
        }
    }
}
