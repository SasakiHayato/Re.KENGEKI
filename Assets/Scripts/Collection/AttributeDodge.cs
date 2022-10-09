using UnityEngine;

public class AttributeDodge : MonoBehaviour
{
    Collider _collider;
    GameObject _parent;

    public void SetData(Collider collider, GameObject parent)
    {
        _collider = collider;
        _parent = parent;
    }

    void Update()
    {
        _collider.enabled = _parent.activeSelf;
        transform.position = _parent.transform.position;
    }

    void OnTriggerEnter(Collider other)
    {
        IDodgeEvent dodgeEvent = other.GetComponent<IDodgeEvent>();

        if (dodgeEvent != null && !dodgeEvent.ExecutionDodgeEvent)
        {
            dodgeEvent.ExecuteDodgeEvent();
        }
    }
}
