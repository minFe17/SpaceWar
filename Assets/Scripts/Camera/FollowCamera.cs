using System.Collections;
using System.Collections.Generic;
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
        GenericSingleton<UIManager>.Instance.FollowCam = _followCam;
    }
}
