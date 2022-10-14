using UnityEngine;

namespace Sounds
{
    public partial class SoundOperator : MonoBehaviour
    {
        /// <summary>
        /// 再生リクエストのトークンデータ
        /// </summary>

        protected struct PlayRequestData
        {
            public string Path { get; private set; } 
            public Transform User { get; private set; }
            public SoundType SoundType { get; private set; }

            public void Set(PlayRequester requester)
            {
                Path = requester.Path;
                User = requester.User;
                SoundType = requester.SoundType;
            }
        }
    }
}
