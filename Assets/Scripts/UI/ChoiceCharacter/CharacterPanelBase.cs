using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPanelBase : MonoBehaviour
{
    [SerializeField] protected ChoiceCharacterUI _parentPanel;
    [SerializeField] protected Image _characterImage;

    protected PlayerInfoData _playerInfoData;
    protected PlayerLevelData _playerLevelData;
    protected StringBuilder _stringBuilder = new StringBuilder();

    public virtual void ShowUI(PlayerInfoData playerInfoData, PlayerLevelData playerLevelData)
    {
        _playerInfoData = playerInfoData;
        _playerLevelData = playerLevelData;
        ShowCharacterImage();
    }

    void ShowCharacterImage()
    {
        _characterImage.sprite = _parentPanel.CharacterImage;
    }
}
