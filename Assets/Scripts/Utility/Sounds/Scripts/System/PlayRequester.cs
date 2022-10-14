using UnityEngine;

namespace Sounds
{
    public partial class SoundOperator : MonoBehaviour
    {
        /// <summary>
        /// �Đ����N�G�X�g�̃g�[�N��
        /// </summary>

        public struct PlayRequester
        {
            public string Path { get; private set; }
            public SoundType SoundType { get; private set; }
            public Transform User { get; private set; }

            /// <summary>
            /// �Đ�����p�X�̃Z�b�g
            /// </summary>
            /// <param name="path"></param>
            /// <returns></returns>
            public PlayRequester SetPath(string path)
            {
                Path = path;
                return this;
            }

            /// <summary>
            /// �Đ�����T�E���h�^�C�v�̃Z�b�g
            /// </summary>
            /// <param name="type"></param>
            /// <returns></returns>
            public PlayRequester SetType(SoundType type)
            {
                SoundType = type;
                return this;
            }

            /// <summary>
            /// �g�p�҂̃Z�b�g
            /// </summary>
            /// <param name="user"></param>
            /// <returns></returns>
            public PlayRequester SetUser(Transform user)
            {
                User = user;
                return this;
            }

            /// <summary>
            /// �\��
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
