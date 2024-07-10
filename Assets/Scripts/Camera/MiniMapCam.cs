using UnityEngine;

public class MiniMapCam : MonoBehaviour
{
    Transform _player;

    public void Init(Transform player)
    {
        _player = player;
    }

    void Update()
    {
        Move();
    }

    public void Move()
    {
        Vector3 miniMapCamPosition = new Vector3(_player.position.x, transform.position.y, _player.position.z);
        transform.position = miniMapCamPosition;
    }
}