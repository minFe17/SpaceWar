using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] GameObject _leftDoor;
    [SerializeField] GameObject _rightDoor;

    [SerializeField] float _speed;
    [SerializeField] Transform _leftDoorArea;
    [SerializeField] Transform _rightDoorArea;

    BoxCollider _collider;
    Vector3 _doorPos;

    bool _isOpen;

    void Start()
    {
        _collider = GetComponent<BoxCollider>();
        _doorPos = _leftDoor.transform.position;
        if(gameObject.GetComponentInParent<DoorList>())
            gameObject.GetComponentInParent<DoorList>()._doors.Add(this._collider);
    }

    void Update()
    {
        if (_isOpen)
            Open();
        else
            Close();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            _isOpen = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
            _isOpen = false;
    }

    public void Open()
    {
        _leftDoor.transform.Translate((_leftDoorArea.position - _leftDoor.transform.position) * Time.deltaTime * _speed, Space.World);
        _rightDoor.transform.Translate((_rightDoorArea.position - _rightDoor.transform.position) * Time.deltaTime * _speed, Space.World);
    }

    public void Close()
    {
        _leftDoor.transform.Translate((_doorPos - _leftDoor.transform.position) * Time.deltaTime * _speed, Space.World);
        _rightDoor.transform.Translate((_doorPos - _rightDoor.transform.position) * Time.deltaTime * _speed, Space.World);
    }

    public void Lock()
    {
        _isOpen = false;
        _leftDoor.transform.position = _doorPos;
        _rightDoor.transform.position = _doorPos;
    }
}
