using UnityEngine;

public class BattleField : MonoBehaviour
{
    public GameManager _gameManager;
    public DoorList _doorList;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            _gameManager.Battle(_doorList);
    }
}