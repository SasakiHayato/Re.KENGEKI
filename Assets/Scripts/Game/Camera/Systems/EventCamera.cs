using UnityEngine;

public class EventCamera : MonoBehaviour
{
    [SerializeField] string _path;

    Camera _camera;

    public string Path => _path;

    void Start()
    {
        _camera = GetComponent<Camera>();
        _camera.enabled = false;

        Skybox skybox = gameObject.AddComponent<Skybox>();
        skybox.material = CameraController.Skybox.material;

        if (CameraController.Infomation != null)
        {
            CameraController.Infomation.AddEventCamera(this);
        }
    }

    public void IsView(bool isView)
    {
        _camera.enabled = isView;
    }
}
