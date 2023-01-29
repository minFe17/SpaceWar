using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public Transform _target;

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
