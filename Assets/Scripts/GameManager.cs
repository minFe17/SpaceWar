using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] int _mapStage;
    [SerializeField] int _levelStage;

    void Start()
    {

    }

    void Update()
    {

    }

    public void Battle(DoorList doorList)
    {
        doorList.LockDoor();
    }

    public void Clear(DoorList doorList)
    {
        doorList.UnlockDoor();
    }

    public void NextStage()
    {
        //SceneManager.LoadScene();
        if(_levelStage >= 5)
        {
            _mapStage++;
            _levelStage = 1;
        }
        else
        {
            _levelStage++;
        }
    }   
}
