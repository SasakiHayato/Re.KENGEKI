using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Sounds.Data
{
    /// <summary>
    /// サウンドのデータベース
    /// </summary>

    [CreateAssetMenu(fileName = "SoundData_Sample")]
    public class SoundDataBase : ScriptableObject
    {
        [SerializeField] SoundType _soundType;
        [SerializeField] List<SoundData> _sounDataList;

        public SoundType SoundType => _soundType;
        public SoundData GetData(string path)
        {
            try
            {
                return _sounDataList.First(s => s.Path == path);
            }
            catch
            {
                Debug.Log($"一致するSoundDataがありませんでした。SoundType:{_soundType}. {path} is not found.");
                return null;
            }
        }
    }
}