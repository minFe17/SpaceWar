using UnityEngine;
using Cinemachine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] CinemachineFreeLook _followCam;

    public void Init(Transform target)
    {
        _followCam.Follow = target;
        _followCam.LookAt = target;
    }

    void Update()
    {
        CameraFOV();
    }

    public void CameraFOV()
    {
        _followCam.m_Lens.FieldOfView = _followCam.m_YAxis.Value * 30 + 60;
        if (_followCam.m_Lens.FieldOfView > 90f)
            _followCam.m_Lens.FieldOfView = 90f;
    }
}