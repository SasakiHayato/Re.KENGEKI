using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] bool _viewLine;
    [SerializeField] float _distance;
    [SerializeField] LayerMask _groundLayer;

    public bool IsGround { get; private set; }

    void Update()
    {
        Vector3 direction = (transform.forward + transform.up * -1).normalized;
        IsGround = Physics.Raycast(transform.position, direction, _distance, _groundLayer);

        if (_viewLine)
        {
            Debug.DrawLine(transform.position, transform.position + direction * _distance, Color.red);
        }
    }
}
