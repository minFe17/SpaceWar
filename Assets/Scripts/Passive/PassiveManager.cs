using System.Threading.Tasks;
using UnityEngine;
using Utils;

public class PassiveManager : MonoBehaviour
{
    // ╫л╠шео
    JsonManager _jsonManager;
    PassiveDataList _dataList = DataSingleton<PassiveDataList>.Instance;

    async Task ReadData()
    {
        if (_jsonManager == null)
            _jsonManager = GenericSingleton<JsonManager>.Instance;
        await _jsonManager.ReadPassiveData();
    }

    public async Task Init()
    {
        await ReadData();
    }

    public void RemovePassive(PassiveData passive)
    {
        _dataList.UIDataList.Remove(passive);
    }
}