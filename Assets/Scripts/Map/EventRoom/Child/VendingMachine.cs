using UnityEngine;
using Utils;

public class VendingMachine: EventRoom
{
    int _cost;
    bool _isBuy;

    void Update()
    {
        Event();
    }

    public override void Init()
    {
        base.Init();
        _isBuy = false;
    }

    public override void OnEnter()
    {
        _message = $"수상한 물약 사기 : {_cost}";
    }

    public override void Event()
    {
        BuyPotion();
    }

    void BuyPotion()
    {
        if(_cost == 0)
            _cost = Random.Range(10, 30);

        if (Input.GetKeyDown(KeyCode.F) && _inPlayer && !_isBuy)
        {
            if(GenericSingleton<PlayerDataManager>.Instance.Money >= _cost)
            {
                GenericSingleton<PlayerDataManager>.Instance.Money -= _cost;
                GenericSingleton<UIManager>.Instance.IngameUI.ShowMoney();
                GenericSingleton<PotionManager>.Instance.ApplyPotion();
                _isBuy = true;
            }
            _inPlayer = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GenericSingleton<UIManager>.Instance.InfoKey.SetActive(false);
            _inPlayer = false;
        }
    }
}