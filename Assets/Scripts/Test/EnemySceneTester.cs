using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySceneTester : MonoBehaviour
{
    [SerializeField] Transform _target;

    void Awake()
    {
        GameManager.Instance.GameUser = _target;
    }
}
