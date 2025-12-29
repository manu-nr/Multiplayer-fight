using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Vector3 _cameraOffset;

    private Camera _mainCamera;
    private Transform _myPlayerTransform;

    public Camera MainCamera => _mainCamera;

    public static CameraController Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }

    private void Start()
    {
        _mainCamera = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        if (_myPlayerTransform == null)
        {
            if(NetworkManager.Instance.MyPlayer != null)
                _myPlayerTransform = NetworkManager.Instance.MyPlayer.transform;
        }

        if(_myPlayerTransform != null)
        {
            gameObject.transform.position = _myPlayerTransform.position - _cameraOffset;
            gameObject.transform.LookAt(_myPlayerTransform.position);
        }

    }
}
