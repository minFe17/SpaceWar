using UnityEngine;
using Cinemachine;
using Utils;

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
        _followCam.m_Lens.FieldOfView = _followCam.m_YAxis.Value * 60 + 30;
        if (_followCam.m_Lens.FieldOfView > 60f)
            _followCam.m_Lens.FieldOfView = 60f;
    }
}