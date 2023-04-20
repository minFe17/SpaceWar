using UnityEngine;
using Utils;

public class Portal : EventRoom
{
    void Update()
    {
        Event();
    }

    public override void OnEnter()
    {
        _message = "다음 스테이지";
    }

    public override void Event()
    {
        if (Input.GetKeyDown(KeyCode.F) && _inPlayer)
        {
            GenericSingleton<GameManager>.Instance.NextStage();
            _inPlayer = false;
        }
    }
}
