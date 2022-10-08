using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BulletData 
{
    public enum ShotType
    {
        ToPlayer,
        Homing,
    }

    [System.Serializable]
    public class Model
    {
        [SerializeField] ShotType _shotType;
        [SerializeField] Transform _point;

        public ShotType ShotType => _shotType;
        public Transform Point => _point;
    }

    [SerializeField] float _moveSpeed;
    [SerializeField] int _attackPower;
    [SerializeField] float _delayTime;
    [SerializeField] List<Model> _modelList;

    public float MoveSpeed => _moveSpeed;
    public int AttackPower => _attackPower;
    public float DelayTime => _delayTime;
    public List<Model> ModelList => _modelList;
}
