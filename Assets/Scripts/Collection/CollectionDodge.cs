using UnityEngine;

public class CollectionDodge : MonoBehaviour
{
    enum ColliderType
    {
        Cube,
        Sphere,

    }

    [SerializeField] ColliderType _type;
    [SerializeField] float _scale = 1;
    
    GameObject _obj;
    Collider _collider;

    void Start()
    {
        _obj = new GameObject("CollectDodge");

        _collider = null;

        switch (_type)
        {
            case ColliderType.Cube: _collider = _obj.AddComponent<BoxCollider>();
                break;
            case ColliderType.Sphere: _collider = _obj.AddComponent<SphereCollider>();
                break;
        }

        _obj.transform.localScale = Vector3.one * _scale;
        _collider.isTrigger = true;

        AttributeDodge attribute = _obj.AddComponent<AttributeDodge>();
        attribute.SetData(_collider, gameObject);
    }
}
