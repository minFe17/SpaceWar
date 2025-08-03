using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Utils;

public class PassiveManager : MonoBehaviour
{
    // �̱���
    JsonManager _jsonManager;
    CommonPassiveData _commonPassiveData;
    List<PlayerPassiveData> _playerPassiveDatas = new List<PlayerPassiveData>();

    List<PassiveData> _allPassiveData;
    Dictionary<EPassiveType, IPassiveEffect> _passiveEffect;

    public List<PassiveData> AllPassiveData { get => _allPassiveData; }
    public List<PlayerPassiveData> PlayerPassiveDatas { get => _playerPassiveDatas; }

    async Task ReadData()
    {
        if (_jsonManager == null)
            _jsonManager = GenericSingleton<JsonManager>.Instance;
        await _jsonManager.ReadPassiveData();
    }

    public async Task Init()
    {
        if (_playerPassiveDatas.Count == 0)
        {
            _playerPassiveDatas.Add(DataSingleton<SoldierPassiveData>.Instance);
            _playerPassiveDatas.Add(DataSingleton<WitchPassiveData>.Instance);
        }
        if (_passiveEffect == null)
            SetPassiveEffect();
        await ReadData();
        SetAllPassiveData();
    }

    /// <summary>
    /// ���� �нú� �����Ϳ� ĳ���ͺ� �нú� �����͸� ��ħ
    /// </summary>
    void SetAllPassiveData()
    {
        if (_commonPassiveData == null)
            _commonPassiveData = DataSingleton<CommonPassiveData>.Instance;

        // ���� �÷��̾� Ÿ���� ������
        EPlayerType playerType = DataSingleton<PlayerData>.Instance.PlayerType;

        // ���� �нú� �����͸� ����Ʈ�� ����
        _allPassiveData = new List<PassiveData>(_commonPassiveData.CommonPassiveDatas);

        // �÷��̾� Ÿ�Կ� �ش��ϴ� ĳ���� �нú� �����͸� ����Ʈ�� �߰�
        _allPassiveData.AddRange(_playerPassiveDatas[(int)playerType].PlayerPassiveDatas);
    }

    void SetPassiveEffect()
    {
        _passiveEffect = new Dictionary<EPassiveType, IPassiveEffect>()
        {
            {EPassiveType.HpUp, new HpUp() },
            {EPassiveType.DamageUp, new DamageUp() },
            {EPassiveType.SpeedUp, new SpeedUp() },
            {EPassiveType.BulletUp, new BulletUp() },
            {EPassiveType.GetMoneyUp, new GetMoneyUp() },
            {EPassiveType.DoubleHp, new DoubleHp() },
            {EPassiveType.HpUpByMoney, new HpUpByMoney() },
            {EPassiveType.Vampirism, new Vampirism() },
            {EPassiveType.UnlockBurstMode, new UnlockBurstMode() },
            {EPassiveType.UnlockAutoMode, new UnlockAutoMode() },
            {EPassiveType.UnlockThunderBall, new UnlockThunderBall() },
            {EPassiveType.UnlockBlackHole, new UnlockBlackHole() }
        };
    }

    public IPassiveEffect GetPassive(EPassiveType key)
    {
        IPassiveEffect passiveEffect;
        _passiveEffect.TryGetValue(key, out passiveEffect);
        return passiveEffect;
    }

    public void RemovePassive(PassiveData passive)
    {
        _allPassiveData.Remove(passive);
    }
}