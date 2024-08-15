using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;
    [SerializeField] private CinemachineVirtualCamera _playerVirtualCamera;

    [SerializeField] private LayerMask _shadowLayer;
    private Transform _currentTarget;


    private void LateUpdate()
    {
        if (_currentTarget != null)
            _virtualCamera.Follow = _currentTarget;
    }

    public void EnterCameraState(Transform target)
    {
        _currentTarget = target;
        _virtualCamera.transform.position = target.position;
        _virtualCamera.transform.rotation = Quaternion.Euler(target.eulerAngles);
        _virtualCamera.Priority = 1;
        _playerVirtualCamera.Priority = 0;
        _camera.cullingMask &= ~_shadowLayer.value;
    }

    public void ExitCameraState()
    {
        _virtualCamera.Priority = 0;
        _playerVirtualCamera.Priority = 1;
        _currentTarget = null;
        _camera.cullingMask |= _shadowLayer.value;
    }

    public void Follow(Transform position)
    {
        _virtualCamera.transform.position = position.position;
        _virtualCamera.transform.rotation = position.rotation;
    }
}
