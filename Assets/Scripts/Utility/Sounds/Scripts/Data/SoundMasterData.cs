using System.Collections.Generic;

namespace Sounds.Data
{
    /// <summary>
    /// Sound�̃}�X�^�[�f�[�^
    /// </summary>

    public static class SoundMasterData
    {
        static public List<string> BGMList { get; private set; }

        static public float MasterVolume { get; private set; }
        static public float BGMVolume { get; private set; }
        static public float SEVolume { get; private set; }

        /// <summary>
        /// �X�V���ꂽ�}�X�^�[�{�����[���̃Z�b�g
        /// </summary>
        /// <param name="volume"></param>
        static public void SetMaster(float volume)
        {
            if (volume > 1)
            {
                MasterVolume = 1;
            }
            else
            {
                MasterVolume = volume;
            }
        }

        /// <summary>
        /// �X�V���ꂽBGM�{�����[���̃Z�b�g
        /// </summary>
        /// <param name="volume"></param>
        static public void SetBGM(float volume)
        {
            if (volume > 1)
            {
                BGMVolume = 1;
            }
            else
            {
                BGMVolume = volume;
            }
        }

        /// <summary>
        /// �X�V���ꂽSE�{�����[���̃Z�b�g
        /// </summary>
        /// <param name="volume"></param>
        static public void SetSE(float volume)
        {
            if (volume > 1)
            {
                SEVolume = 1;
            }
            else
            {
                SEVolume = volume;
            }
        }

        /// <summary>
        /// �Đ������BGM�̃p�X���Z�b�g
        /// </summary>
        /// <param name="path"></param>
        static public void SetBGMPath(string path)
        {
            BGMList.Add(path);
        }

        /// <summary>
        /// BGMList�̏�����
        /// </summary>
        static public void InitalizeList()
        {
            BGMList = new List<string>();
        }

        /// <summary>
        /// �}�X�^�[�f�[�^�̏�����
        /// </summary>
        static public void Initalize()
        {
            BGMList = new List<string>();
            MasterVolume = 0;
            BGMVolume = 0;
            SEVolume = 0;
        }
    }
}