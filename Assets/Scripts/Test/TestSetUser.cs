using UnityEngine;

public class TestSetUser : MonoBehaviour
{
    [SerializeField] Transform _target;

    void Awake()
    {
        GameManager.Instance.GameUser = _target;
    }
}
