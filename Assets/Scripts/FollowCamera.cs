using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] CinemachineFreeLook _followCam;

    public void Init(Transform target)
    {
        _followCam.Follow = target;
        _followCam.LookAt = target;
        GenericSingleton<UIManager>.GetInstance().SetFollowCam(_followCam);
    }
}
