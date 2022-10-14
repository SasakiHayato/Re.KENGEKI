using System.Collections.Generic;
using UnityEngine;
using Sounds.Data;

namespace Sounds
{
    public partial class SoundOperator : MonoBehaviour
    {
        /// <summary>
        /// ��~����ۂ̃g�[�N��
        /// </summary>

        public struct StopRequester
        {
            bool _isCreate;

            public bool IsAll { get; private set; }
            public List<string> PathList { get; private set; }

            /// <summary>
            /// �g�[�N���̐���
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
            /// ���ݍĐ�����BGM���~�߂�
            /// </summary>
            /// <returns></returns>
            public StopRequester CurrentBGM()
            {
                if (!_isCreate)
                {
                    Debug.LogWarning("�f�[�^����������Ă��܂���B");
                }

                foreach (string bgm in SoundMasterData.BGMList)
                {
                    PathList.Add(bgm);
                }

                SoundMasterData.InitalizeList();

                return this;
            }

            /// <summary>
            /// �w�肳�ꂽSound���~�߂�
            /// </summary>
            /// <param name="path"></param>
            /// <returns></returns>
            public StopRequester SetPath(string path)
            {
                if (!_isCreate)
                {
                    Debug.LogWarning("�f�[�^����������Ă��܂���B");
                }

                PathList.Add(path);

                return this;
            }

            /// <summary>
            /// �S�Ď~�߂�
            /// </summary>
            /// <returns></returns>
            public StopRequester SetAll()
            {
                IsAll = true;

                return this;
            }

            /// <summary>
            /// �\��
            /// </summary>
            public void Request()
            {
                Instance.StopRequest(this);
            }
        }
    }
}
