using UnityEngine;

public class PageButtonPanel : MonoBehaviour
{
    [SerializeField] ChoiceCharacterUI _parentPanel;

    const int Left = -1;
    const int Right = 1;

    public void MoveLeftPage()
    {
        if (_parentPanel.Index == (int)EPlayerType.Soldier)
            return;
        _parentPanel.ChangePage(Left);
    }

    public void MoveRightPage() 
    {
        if (_parentPanel.Index == (int)EPlayerType.Max-1)
            return;
        _parentPanel.ChangePage(Right);
    }
}