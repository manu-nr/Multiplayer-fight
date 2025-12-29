using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Vector3 _cameraOffset;

    private Camera _mainCamera;
    private Transform _myPlayerTransform;

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
