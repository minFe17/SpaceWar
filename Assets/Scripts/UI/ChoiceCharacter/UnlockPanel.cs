using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utils;

public class UnlockPanel : CharacterPanelBase
{
    [SerializeField] List<Text> _skillNameText;
    [SerializeField] Text _statText;
    [SerializeField] Text _upgradeText;
    [SerializeField] Text _upgradeCostText;

    public override void ShowUI(PlayerInfoData playerInfoData, PlayerLevelData playerLevelData)
    {
        base.ShowUI(playerInfoData, playerLevelData);
        ShowSkill();
        ShowStat();
        ShowUpgradeStat();
    }

    void ShowSkill()
    {
        for (int i = 0; i < _skillNameText.Count; i++)
            _skillNameText[i].text = _playerInfoData.SkillName[i];
    }

    void ShowStat()
    {
        PlayerStatData playerStatData = _playerInfoData.StatDataList[_playerLevelData.Level];
        _stringBuilder.Append($"HP : {playerStatData.MaxHp}\n");
        _stringBuilder.Append($"공격력 : {playerStatData.BulletDamage}\n");
        _stringBuilder.Append($"탄약 : {playerStatData.MaxBullet}");

        _statText.text = _stringBuilder.ToString();
        _stringBuilder.Clear();
    }

    void ShowUpgradeStat()
    {
        PlayerStatData playerStatData = _playerInfoData.StatDataList[_playerLevelData.Level];
        PlayerStatData nextStatData = null;

        if (_playerInfoData.StatDataList.Count-1 > _playerLevelData.Level)
        {
            nextStatData = _playerInfoData.StatDataList[_playerLevelData.Level + 1];
            _stringBuilder.Append($"HP : {playerStatData.MaxHp} => {nextStatData.MaxHp}\n");
            _stringBuilder.Append($"공격력 : {playerStatData.BulletDamage} =>  {nextStatData.BulletDamage}\n");
            _stringBuilder.Append($"탄약 : {playerStatData.MaxBullet} =>  {nextStatData.MaxBullet}");
            _upgradeCostText.text = $"비용 : {playerStatData.UpgradeCost}";
        }
        else
        {
            _stringBuilder.Append($"HP : {playerStatData.MaxHp} => ---\n");
            _stringBuilder.Append($"공격력 : {playerStatData.BulletDamage} => ---\n");
            _stringBuilder.Append($"탄약 : {playerStatData.MaxBullet} => ---");
            _upgradeCostText.text = $"비용 : ---";
        }

        _upgradeText.text = _stringBuilder.ToString();
        _stringBuilder.Clear();
    }

    public void Upgrade()
    {
        PlayerStatData playerStatData = _playerInfoData.StatDataList[_playerLevelData.Level];
        GemData gemData = DataSingleton<GemData>.Instance;
        if (playerStatData.UpgradeCost > gemData.Gem)
            return;
        if (_playerInfoData.StatDataList.Count-1 <= _playerLevelData.Level)
            return;
        _playerLevelData.Level++;
        gemData.Gem -= playerStatData.UpgradeCost;
        GenericSingleton<JsonManager>.Instance.WrtiePlayerLevelDataFile();
    }

    public async void GameStart()
    {
        GenericSingleton<JsonManager>.Instance.DestroyDataFiles();
        DataSingleton<GameData>.Instance.MapStage = 1;
        await GenericSingleton<WorldManager>.Instance.ResetWorld();
        PlayerData playerData = DataSingleton<PlayerData>.Instance;
        playerData.PlayerType = (EPlayerType)_parentPanel.Index;
        SceneManager.LoadScene("FirstWorld");
    }
}
