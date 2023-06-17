using UnityEngine;
using Utils;

public class VendingMachine: MonoBehaviour
{
    int _cost;
    bool _inPlayer;
    bool _isBuy;

    void Update()
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

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GenericSingleton<UIManager>.Instance.InfoKey.SetActive(true);
            GenericSingleton<UIManager>.Instance.InfoMessage.text = $"수상한 물약 사기 : {_cost}";
            _inPlayer = true;
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