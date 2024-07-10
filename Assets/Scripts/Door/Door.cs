using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] GameObject _leftDoor;
    [SerializeField] GameObject _rightDoor;
    [SerializeField] Transform _leftDoorArea;
    [SerializeField] Transform _rightDoorArea;

    [SerializeField] float _speed;

    BoxCollider _collider;
    Vector3 _doorPos;

    bool _isOpen;

    void Start()
    {
        _collider = GetComponent<BoxCollider>();
        _doorPos = _leftDoor.transform.position;
        if (gameObject.GetComponentInParent<DoorList>())
            gameObject.GetComponentInParent<DoorList>().Doors.Add(this);
    }

    void Update()
    {
        if (_isOpen)
            Open();
        else
            Close();
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
        _collider.enabled = false;
        _isOpen = false;
        _leftDoor.transform.position = _doorPos;
        _rightDoor.transform.position = _doorPos;
    }

    public void Unlock()
    {
        _collider.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            _isOpen = true;
        else if (other.gameObject.CompareTag("Wall"))
            Destroy(other.transform.parent.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            _isOpen = false;
    }
}