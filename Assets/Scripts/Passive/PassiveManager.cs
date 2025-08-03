using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Utils;

public class PassiveManager : MonoBehaviour
{
    // 싱글턴
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
    /// 공통 패시브 데이터와 캐릭터별 패시브 데이터를 합침
    /// </summary>
    void SetAllPassiveData()
    {
        if (_commonPassiveData == null)
            _commonPassiveData = DataSingleton<CommonPassiveData>.Instance;

        // 현재 플레이어 타입을 가져옴
        EPlayerType playerType = DataSingleton<PlayerData>.Instance.PlayerType;

        // 공통 패시브 데이터를 리스트에 복사
        _allPassiveData = new List<PassiveData>(_commonPassiveData.CommonPassiveDatas);

        // 플레이어 타입에 해당하는 캐릭터 패시브 데이터를 리스트에 추가
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