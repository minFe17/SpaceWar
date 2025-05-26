using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

public class ChoiceCharacterUI : MonoBehaviour
{
    public async void GameStart()
    {
        GenericSingleton<JsonManager>.Instance.DestroyDataFiles();
        DataSingleton<GameData>.Instance.MapStage = 1;
        await GenericSingleton<WorldManager>.Instance.ResetWorld();
        SceneManager.LoadScene("FirstWorld");
        // 플레이어 캐릭터가 뭔지 넘겨주기
    }
}
