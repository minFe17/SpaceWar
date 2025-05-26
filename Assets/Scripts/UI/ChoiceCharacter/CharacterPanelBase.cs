using UnityEngine;
using UnityEngine.UI;

public class CharacterPanelBase : MonoBehaviour
{
    [SerializeField] protected ChoiceCharacterUI _parentPanel;
    [SerializeField] protected Image _characterImage;

    PlayerInfoData _playerInfoData;
    PlayerLevelData _playerLevelData;

    public void ShowUI(PlayerInfoData playerInfoData, PlayerLevelData playerLevelData)
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
