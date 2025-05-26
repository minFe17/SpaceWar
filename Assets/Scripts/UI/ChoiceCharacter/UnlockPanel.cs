using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

public class UnlockPanel : MonoBehaviour
{
    PlayerInfoData _playerInfoData;
    PlayerLevelData _playerLevelData;

    public async void GameStart()
    {
        GenericSingleton<JsonManager>.Instance.DestroyDataFiles();
        DataSingleton<GameData>.Instance.MapStage = 1;
        await GenericSingleton<WorldManager>.Instance.ResetWorld();
        SceneManager.LoadScene("FirstWorld");
        // 플레이어 캐릭터가 뭔지 넘겨주기
    }

    public void ShowUI(PlayerInfoData playerInfoData, PlayerLevelData playerLevelData)
    {
        _playerInfoData = playerInfoData;
        _playerLevelData = playerLevelData;
    }
}
