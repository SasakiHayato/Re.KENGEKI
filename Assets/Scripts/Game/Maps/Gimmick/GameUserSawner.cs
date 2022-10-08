using UnityEngine;

public class GameUserSawner : MonoBehaviour
{
    [SerializeField] Transform _point;
    [SerializeField] GameObject _user;

    void Start()
    {
        Transform user = Instantiate(_user).transform;
        user.position = _point.position;

        GameManager.Instance.GameUser = user;
    }
}
