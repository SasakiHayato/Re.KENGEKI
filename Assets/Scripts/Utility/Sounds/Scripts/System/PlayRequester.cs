using UnityEngine;

namespace Sounds
{
    public partial class SoundOperator : MonoBehaviour
    {
        /// <summary>
        /// 再生リクエストのトークン
        /// </summary>

        public struct PlayRequester
        {
            public string Path { get; private set; }
            public SoundType SoundType { get; private set; }
            public Transform User { get; private set; }

            /// <summary>
            /// 再生するパスのセット
            /// </summary>
            /// <param name="path"></param>
            /// <returns></returns>
            public PlayRequester SetPath(string path)
            {
                Path = path;
                return this;
            }

            /// <summary>
            /// 再生するサウンドタイプのセット
            /// </summary>
            /// <param name="type"></param>
            /// <returns></returns>
            public PlayRequester SetType(SoundType type)
            {
                SoundType = type;
                return this;
            }

            /// <summary>
            /// 使用者のセット
            /// </summary>
            /// <param name="user"></param>
            /// <returns></returns>
            public PlayRequester SetUser(Transform user)
            {
                User = user;
                return this;
            }

            /// <summary>
            /// 申請
            /// </summary>
            public void Request()
            {
                PlayRequestData requestData = new PlayRequestData();
                requestData.Set(this);

                Instance.PlayRequest(requestData);
            }
        }
    }
}
