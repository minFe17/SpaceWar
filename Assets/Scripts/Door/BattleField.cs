using UnityEngine;

public class BattleField : MonoBehaviour
{
    public GameManager _gameManager;
    public DoorList _doorList;
    public EnemyController _enemyController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _gameManager.Battle(_doorList);
            this.GetComponent<BoxCollider>().enabled = false;
            _enemyController.gameObject.SetActive(true);

        }
    }

    public void Clear()
    {
        _gameManager.Clear(_doorList);
    }
}