using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] UIManager _uiManager;
    int _mapStage;
    int _levelStage;

    void Start()
    {
        _mapStage = 1;
        _levelStage = 1;
        _uiManager.ShowStage(_mapStage, _levelStage);
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
        _uiManager.ShowStage(_mapStage, _levelStage);

    }
}
