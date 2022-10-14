using UnityEngine;

namespace Sounds.Data
{
    /// <summary>
    /// サウンドのデータ
    /// </summary>

    [System.Serializable]
    public class SoundData
    {
        [SerializeField] string _path;
        [SerializeField] AudioClip _clip;
        [SerializeField, Range(0, 1)] float _volume;
        [SerializeField, Range(0, 1)] int _spatialBlend;

        public string Path => _path;
        public AudioClip AudioClip => _clip;
        public float Volume => _volume;
        public int SpatialBlend => _spatialBlend;
    }
}