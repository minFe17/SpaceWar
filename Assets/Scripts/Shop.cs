using System.Collections.Generic;
using UnityEngine;
using Utils;

public class Shop : MonoBehaviour
{
    List<PotionBase> _potions = new List<PotionBase>();
    Player _player; //플레이어 받아와야함
    int _cost;
    bool _inPlayer;

    void Update()
    {
        BuyPotion();
    }

    void BuyPotion()
    {
        if(_cost == 0)
            _cost = Random.Range(10, 30);

        if (Input.GetKeyDown(KeyCode.F) && _inPlayer)
        {
            if(_player.Money >= _cost)
            {
                ApplyPotion();
                _player.Money -= _cost;
            }
            _inPlayer = false;
        }
    }

    void ApplyPotion()
    {
        if (_potions.Count == 0)
            AddPotion();
        int random = Random.Range(0, _potions.Count);
        _potions[random].PotionEffect();
    }

    void AddPotion()
    {
        _potions.Add(new RecoveryPotion());
        _potions.Add(new DamagePotion());
        _potions.Add(new MaxHPIncrease());
        _potions.Add(new MaxHPReduced());
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GenericSingleton<UIManager>.Instance.InfoKey.SetActive(true);
            GenericSingleton<UIManager>.Instance.InfoMessage.text = $"수상한 물약 사기 : {_cost}";
            _inPlayer = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GenericSingleton<UIManager>.Instance.InfoKey.SetActive(false);
            _inPlayer = false;
        }
    }
}