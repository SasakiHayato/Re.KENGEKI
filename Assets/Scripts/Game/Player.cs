using UnityEngine;

/// <summary>
/// プレイヤーの管理クラス
/// </summary>

public class Player : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 1;

    Vector3 _beforePos;

    Rigidbody _rb;
    InputOperator _inputOperator;

    readonly float Gravity = Physics.gravity.y; 
    
    void Awake()
    {
        _inputOperator = new InputOperator();
        _inputOperator.Enable();
    }

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _beforePos = transform.position;
    }

    void Update()
    {
        Move(_inputOperator.Player.Move.ReadValue<Vector2>());
        Rotate();
    }

    void OnDestroy()
    {
        _inputOperator.Disable();
    }

    void Move(Vector2 dir)
    {
        Vector3 move;

        if (CameraController.Data != null)
        {
            Vector3 forward = CameraController.Data.Foward * dir.y;
            Vector3 right = CameraController.Data.Right * dir.x;

            move = (forward + right) * _moveSpeed;
        }
        else
        {
            move = new Vector3(dir.x, 0, dir.y) * _moveSpeed;
        }

        move.y = Gravity;
        _rb.velocity = move;
    }

    void Rotate()
    {
        Vector3 diff = transform.position - _beforePos;
        diff.y = 0;

        if (diff.magnitude > 0.01f)
        {
            transform.rotation = Quaternion.LookRotation(diff);
        }

        _beforePos = transform.position;
    }
}
