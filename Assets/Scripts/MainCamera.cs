using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainCamera : MonoBehaviour
{
    public Transform _target;

    void Start()
    {
    
    }

    void Update()
    {
        FollowCamera();
    }

    void FollowCamera()
    {
        transform.position = _target.position;
    }

    public void Rotate(Vector3 rotate)
    {
        transform.eulerAngles = rotate;
    }

}
